using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.Application.Base;
using ThreadSafeNumericIdGenerator.DataContract;
using ThreadSafeNumericIdGenerator.Domain.Model;

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

            if (await idHolderService.ExistsAsync(createIdHolderDto.Name))
                return BadRequest($"Name {createIdHolderDto.Name} is in use");

            await idHolderService.CreateAsync(createIdHolderDto);

            return Created("", createIdHolderDto);
        }
    }
}

