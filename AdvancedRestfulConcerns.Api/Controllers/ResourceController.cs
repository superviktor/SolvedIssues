using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AdvancedRestfulConcerns.Api.ActionConstraints;
using AdvancedRestfulConcerns.Api.Contract;
using AdvancedRestfulConcerns.Api.Helpers;
using AdvancedRestfulConcerns.Api.Model;
using AdvancedRestfulConcerns.Api.Persistence;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace AdvancedRestfulConcerns.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ResponseCache(CacheProfileName = "240SecsCacheProfile")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Public)]
    [HttpCacheValidation(MustRevalidate = false)]
    public class ResourceController : ControllerBase
    {
        private readonly IPropertyCheckerService _propertyCheckerService;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IResourceRepository _repository;

        public ResourceController(
            IResourceRepository repository,
            IPropertyMappingService propertyMappingService,
            IPropertyCheckerService propertyCheckerService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _propertyMappingService =
                propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService =
                propertyCheckerService ?? throw new ArgumentNullException(nameof(propertyCheckerService));
        }

        [HttpGet(Name = "GetResources")]
        //[ResponseCache(Duration = 120)]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public IActionResult GetAll([FromQuery] GetResources getResources)
        {
            if (!_propertyMappingService.ValidMappingExistsFor<Resource, ResourceDto>(getResources.OrderBy))
                return BadRequest();

            if (!_propertyCheckerService.TypeHasProperties<ResourceDto>(getResources.Fields))
                return BadRequest();

            var resources = _repository.GetAll(getResources.PageNumber, getResources.PageSize, getResources.OrderBy);
            var dtos = resources.Select(r => new ResourceDto
            {
                Id = r.Id,
                Name = $"{r.FirstName} {r.LastName}",
                Age = DateTime.Now.Year - r.DateOfBirth.Year
            });

            var paginationMetadata = new
            {
                totalCount = resources.TotalCount,
                pageSize = resources.PageSize,
                currentPage = resources.CurrentPage,
                totalPages = resources.TotalPages
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            var shapedResources = dtos.ShapeData(getResources.Fields);
            var shapedResourcesWithLinks = shapedResources.Select(r =>
            {
                var resourceAsDictionary = r as IDictionary<string, object>;
                var resourceLinks = GetLinksForResource((Guid)resourceAsDictionary["Id"], null);
                resourceAsDictionary.Add("links", resourceLinks);
                return resourceAsDictionary;
            });

            var links = GetLinksForResources(getResources, resources.HasPrevious, resources.HasNext);
            var linkedCollectionResource = new
            {
                value = shapedResourcesWithLinks,
                links
            };

            return Ok(linkedCollectionResource);
        }

        [Produces("application/json",
            "application/vnd.vendorname.hateoas+json",
            "application/vnd.vendorname.resource.full+json",
            "application/vnd.vendorname.resource.full.hateoas+json",
            "application/vnd.vendorname.resource.friendly+json",
            "application/vnd.vendorname.resource.friendly.hateoas+json")]
        [HttpGet("{resourceId}", Name = "GetResource")]
        public IActionResult GetResource(Guid resourceId, string fields, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
                return BadRequest();

            if (!_propertyCheckerService.TypeHasProperties<ResourceDto>(fields))
                return BadRequest();

            var resource = _repository.GetAll(resourceId);

            if (resource == null)
                return NotFound();

            var includeLinks =
                parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
            IEnumerable<LinkDto> links = new List<LinkDto>();
            if (includeLinks)
                links = GetLinksForResource(resourceId, fields);
            var primaryMediaType = includeLinks
                ? parsedMediaType.SubTypeWithoutSuffix.Substring(0, parsedMediaType.SubTypeWithoutSuffix.Length - 8)
                : parsedMediaType.SubTypeWithoutSuffix;

            if (primaryMediaType == "vnd.vendorname.resource.full")
            {
                var fullDto = new ResourceFullDto
                {
                    Id = resource.Id,
                    FirstName = resource.FirstName,
                    LastName = resource.LastName,
                    DateOfBirth = resource.DateOfBirth,
                    DateOfDeath = resource.DateOfDeath
                };

                var shapedFullResourceDto = fullDto.ShapeData(fields);
                if (includeLinks)
                    shapedFullResourceDto.TryAdd("links", links);

                return Ok(shapedFullResourceDto);
            }

            var dto = ResourceToDto(resource);

            var shapedResourceDto = dto.ShapeData(fields);
            if (includeLinks)
                shapedResourceDto.TryAdd("links", links);

            return Ok(shapedResourceDto);
        }

        private ResourceDto ResourceToDto(Resource resource)
        {
            return new ResourceDto
            {
                Id = resource.Id,
                Name = $"{resource.FirstName} {resource.LastName}",
                Age = resource.DateOfDeath == null
                    ? DateTime.Now.Year - resource.DateOfBirth.Year
                    : resource.DateOfDeath.Value.Year - resource.DateOfBirth.Year
            };
        }

        [HttpDelete("{resourceId}", Name = "DeleteResource")]
        public IAsyncResult DeleteResource(Guid resourceId)
        {
            throw new NotImplementedException();
        }

        [HttpPost(Name = "CreateResourceWithDateOfDeath")]
        [RequestHeaderMatchesMediaType("Content-Type",
            "application/vnd.vendorname.resourceforcreationwithdateofdeach+json")]
        [Consumes("application/vnd.vendorname.resourceforcreationwithdateofdeach+json")]
        public IActionResult CreateResourceWithDateOfDeath(ResourceCreateDtoWithDateOfDeath resourceCreateDtoWithDateOfDeath)
        {
            var resource = new Resource(Guid.NewGuid(), resourceCreateDtoWithDateOfDeath.FirstName, resourceCreateDtoWithDateOfDeath.LastName,
                resourceCreateDtoWithDateOfDeath.DateOfBirth, resourceCreateDtoWithDateOfDeath.DateOfDeath);
            _repository.Create(resource);

            var resourceDto = ResourceToDto(resource);

            var links = GetLinksForResource(resourceDto.Id, null);
            var shapedResource = resourceDto.ShapeData(null);
            shapedResource.TryAdd("links", links);

            return Created("CreateResource", new { resourceId = resourceDto.Id, resource = shapedResource });
        }

        [HttpPost(Name = "CreateResource")]
        [RequestHeaderMatchesMediaType("Content-Type", 
            "application/json", 
            "application/vnd.vendorname.resourceforcreation+json")]
        [Consumes("application/json",
            "application/vnd.vendorname.resourceforcreation+json")]
        public IActionResult CreateResource(ResourceCreateDto resourceCreateDto)
        {
            var resource = new Resource(Guid.NewGuid(), resourceCreateDto.FirstName, resourceCreateDto.LastName,
                resourceCreateDto.DateOfBirth, null);
            _repository.Create(resource);

            var resourceDto = ResourceToDto(resource);

            var links = GetLinksForResource(resourceDto.Id, null);
            var shapedResource = resourceDto.ShapeData(null);
            shapedResource.TryAdd("links", links);

            return Created("CreateResource", new { resourceId = resourceDto.Id, resource = shapedResource });
        }

        private string CreateUri(GetResources getResources, ResourceUriType type)
        {
            return type switch
            {
                ResourceUriType.PreviousPage => Url.Link("GetResources",
                    new
                    {
                        pageNumber = getResources.PageNumber - 1,
                        pageSize = getResources.PageSize,
                        orderBy = getResources.OrderBy,
                        fields = getResources.Fields
                    }),
                ResourceUriType.NextPage => Url.Link("GetResources",
                    new
                    {
                        pageNumber = getResources.PageNumber + 1,
                        pageSize = getResources.PageSize,
                        getResources.OrderBy,
                        fields = getResources.Fields
                    }),
                _ => Url.Link("GetResources",
                    new
                    {
                        pageNumber = getResources.PageNumber,
                        pageSize = getResources.PageSize,
                        getResources.OrderBy,
                        fields = getResources.Fields
                    })
            };
        }

        private IEnumerable<LinkDto> GetLinksForResource(Guid resourceId, string fields)
        {
            var links = new List<LinkDto>();

            links.Add(string.IsNullOrWhiteSpace(fields)
                ? new LinkDto(Url.Link("GetResource", new { resourceId }), "self", "GET")
                : new LinkDto(Url.Link("GetResource", new { resourceId, fields }), "self", "GET"));

            links.Add(new LinkDto(Url.Link("DeleteResource", new { resourceId }), "delete-resource", "DELETE"));

            return links;
        }

        private IEnumerable<LinkDto> GetLinksForResources(GetResources getResources, bool hasPrevious, bool hasNext)
        {
            var links = new List<LinkDto>();

            links.Add(new LinkDto(CreateUri(getResources, ResourceUriType.Current), "self", "GET"));
            if (hasPrevious)
                links.Add(new LinkDto(CreateUri(getResources, ResourceUriType.PreviousPage), "previousPage", "GET"));
            if (hasNext)
                links.Add(new LinkDto(CreateUri(getResources, ResourceUriType.NextPage), "nextPage", "GET"));

            return links;
        }
    }
}