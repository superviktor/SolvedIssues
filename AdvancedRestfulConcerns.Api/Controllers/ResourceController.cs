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
        public ResourceController(IResourceRepository repository, IPropertyMappingService propertyMappingService)
        {
            this._repository = repository;
            _propertyMappingService = propertyMappingService;
        }

        [HttpGet(Name = "GetResources")]
        public IActionResult GetAll([FromQuery] GetResources getResources)
        {
            if (!_propertyMappingService.ValidMappingExistsFor<Resource, ResourceDto>(getResources.OrderBy))
                return BadRequest();

            var resources = _repository.Get(getResources.PageNumber, getResources.PageSize, getResources.OrderBy);
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

            return Ok(dtos);
        }

        private string CreateUri(GetResources getResources, ResourceUriType type)
        {
            return type switch
            {
                ResourceUriType.PreviousPage => Url.Link("GetResources",
                    new { pageNumber = getResources.PageNumber - 1, pageSize = getResources.PageSize, orderBy = getResources.OrderBy }),
                ResourceUriType.NextPage => Url.Link("GetResources",
                    new { pageNumber = getResources.PageNumber + 1, pageSize = getResources.PageSize, getResources.OrderBy }),
                _ => Url.Link("GetResources",
                    new { pageNumber = getResources.PageNumber, pageSize = getResources.PageSize, getResources.OrderBy })
            };
        }
    }
}
