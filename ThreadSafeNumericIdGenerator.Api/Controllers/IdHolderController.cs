using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.Api.Common;
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
            var result = await idHolderService.NextAsync(name);
           
            return result.IsSuccess ? Ok(Envelope.Ok(result.Value)) : StatusCode(500, Envelope.Error(result.Error));
        }

        [HttpPost]
        [Route("/api/id-holders")]
        public async Task<IActionResult> Create([FromBody] CreateIdHolderDto createIdHolderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = await idHolderService.ExistsAsync(createIdHolderDto.Name);
            if (exists)
                return BadRequest(Envelope.Error($"IdHolder Name { createIdHolderDto.Name } is in use"));

            var result = await idHolderService.CreateAsync(createIdHolderDto);

            return result.IsSuccess ? Created("", Envelope.Ok(createIdHolderDto)) : StatusCode(500, Envelope.Error(result.Error));
        }
    }
}

