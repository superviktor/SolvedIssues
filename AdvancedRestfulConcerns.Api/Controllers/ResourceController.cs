using System;
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
                Name = $"{r.FirstName} {r.LastName}",
                Age = DateTime.Now.Year - r.DateOfBirth.Year
            });

            var previousPageLink = resources.HasPrevious ? CreateUri(getResources, ResourceUriType.PreviousPage) : null;

            var nextPageLink = resources.HasNext ? CreateUri(getResources, ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = resources.TotalCount,
                pageSize = resources.PageSize,
                currentPage = resources.CurrentPage,
                totalPages = resources.TotalPages,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(dtos.ShapeData(getResources.Fields));
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
                Name = $"{resource.FirstName} {resource.LastName}",
                Age = DateTime.Now.Year - resource.DateOfBirth.Year
            };

            return Ok(dto.ShapeData(fields));
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
    }
}
