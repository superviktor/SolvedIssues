using System.Collections.Generic;
using AdvancedRestfulConcerns.Api.Contract;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedRestfulConcerns.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot()
        {
            var links = new List<LinkDto>
            {
                new LinkDto(Url.Link("GetRoot", new { }), "self", "GET"),
                new LinkDto(Url.Link("GetResources", new { }), "authors", "GET")
            };

            return Ok(links);
        }
    }
}
