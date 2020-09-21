using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AdvancedRestfulConcerns.Api.Contract;
using AdvancedRestfulConcerns.Api.Helpers;
using AdvancedRestfulConcerns.Api.Model;
using AdvancedRestfulConcerns.Api.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedRestfulConcerns.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceRepository _repository;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;
        public ResourceController(
            IResourceRepository repository,
            IPropertyMappingService propertyMappingService,
            IPropertyCheckerService propertyCheckerService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService = propertyCheckerService ?? throw new ArgumentNullException(nameof(propertyCheckerService));
        }

        [HttpGet(Name = "GetResources")]
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
                var resourceLinks = GetLinksForResource((Guid) resourceAsDictionary["Id"], null);
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

        [HttpGet("{resourceId}", Name = "GetResource")]
        public IActionResult GetResource(Guid resourceId, string fields)
        {
            if (!_propertyCheckerService.TypeHasProperties<ResourceDto>(fields))
                return BadRequest();

            var resource = _repository.GetAll(resourceId);

            if (resource == null)
                return NotFound();

            var dto = new ResourceDto
            {
                Id = resource.Id,
                Name = $"{resource.FirstName} {resource.LastName}",
                Age = DateTime.Now.Year - resource.DateOfBirth.Year
            };


            var links = GetLinksForResource(resourceId, fields);
            var linkedResourcesToReturn = dto.ShapeData(fields);
            linkedResourcesToReturn.TryAdd("links", links);

            return Ok(linkedResourcesToReturn);
        }

        [HttpDelete("{resourceId}", Name = "DeleteResource")]
        public IAsyncResult DeleteResource(Guid resourceId)
        {
            throw new NotImplementedException();
        }

        private string CreateUri(GetResources getResources, ResourceUriType type)
        {
            return type switch
            {
                ResourceUriType.PreviousPage => Url.Link("GetResources",
                    new { pageNumber = getResources.PageNumber - 1, pageSize = getResources.PageSize, orderBy = getResources.OrderBy, fields = getResources.Fields }),
                ResourceUriType.NextPage => Url.Link("GetResources",
                    new { pageNumber = getResources.PageNumber + 1, pageSize = getResources.PageSize, getResources.OrderBy, fields = getResources.Fields }),
                _ => Url.Link("GetResources",
                    new { pageNumber = getResources.PageNumber, pageSize = getResources.PageSize, getResources.OrderBy, fields = getResources.Fields })
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
            if(hasPrevious)
                links.Add(new LinkDto(CreateUri(getResources, ResourceUriType.PreviousPage), "previousPage", "GET"));
            if(hasNext)
                links.Add(new LinkDto(CreateUri(getResources, ResourceUriType.NextPage), "nextPage", "GET"));

            return links;
        }
    }
}
