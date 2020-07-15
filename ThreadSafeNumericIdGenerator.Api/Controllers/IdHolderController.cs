using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.Application.Base;
using ThreadSafeNumericIdGenerator.DataContract;

namespace ThreadSafeNumericIdGenerator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdHolderController : ControllerBase
    {
        private readonly IIdHolderService idHolderService;

        public IdHolderController(IIdHolderService idHolderService)
        {
            this.idHolderService = idHolderService;
        }

        [HttpGet]
        [Route("/api/id-holders/{name}/next")]
        public async Task<IActionResult> Next([FromRoute] string name)
        {
            var nextId = await idHolderService.NextAsync(name);

            return Ok(nextId);
        }

        [HttpPost]
        [Route("/api/id-holders")]
        public async Task<IActionResult> Create([FromBody] CreateIdHolderDto createIdHolderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //try catch ?
            await idHolderService.CreateAsync(createIdHolderDto);

            return Created("", createIdHolderDto);
        }
    }
}

