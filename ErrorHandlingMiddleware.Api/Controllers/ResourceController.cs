using ErrorHandlingMiddleware.Api.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ErrorHandlingMiddleware.Api.Controllers
{
    public class ResourceController : ControllerBase
    {
        [HttpGet]
        [Route("api/resources/{resourceId}")]
        public IActionResult GetById(int resourceId)
        {
            //https://localhost:5001/api/resources/123
            //emulate exception
            throw new NotFoundException($"Resource {resourceId} not found");
        }
    }
}
