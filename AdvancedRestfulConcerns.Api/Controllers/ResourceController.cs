using System.Text.Json;
using AdvancedRestfulConcerns.Api.Contract;
using AdvancedRestfulConcerns.Api.Helpers;
using AdvancedRestfulConcerns.Api.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedRestfulConcerns.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceRepository _repository;

        public ResourceController(IResourceRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet(Name = "GetResources")]
        public IActionResult GetAll([FromQuery] GetResources getResources)
        {
            var resources = _repository.Get(getResources.PageNumber, getResources.PageSize);

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

            return Ok(resources);
        }

        private string CreateUri(GetResources getResources, ResourceUriType type)
        {
            return type switch
            {
                ResourceUriType.PreviousPage => Url.Link("GetResources",
                    new {pageNumber = getResources.PageNumber - 1, pageSize = getResources.PageSize,}),
                ResourceUriType.NextPage => Url.Link("GetResources",
                    new {pageNumber = getResources.PageNumber + 1, pageSize = getResources.PageSize,}),
                _ => Url.Link("GetResources",
                    new {pageNumber = getResources.PageNumber, pageSize = getResources.PageSize,})
            };
        }
    }
}
