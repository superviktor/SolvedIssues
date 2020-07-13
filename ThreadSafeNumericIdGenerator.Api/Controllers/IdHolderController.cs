using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.Application.Base;

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
        [Route("/api/id-holers/{name}/next")]
        public async Task<IActionResult> Get([FromRoute] string name)
        {
            var nextId = await idHolderService.Next(name);

            return Ok(nextId);
        }
    }
}
