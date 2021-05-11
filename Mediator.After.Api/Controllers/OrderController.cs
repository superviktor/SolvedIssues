using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mediator.After.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("makeorder")]
        public async Task<IActionResult> MakeOrder([FromBody] MakeOrderRequestModel requestModel)
        {
            var response = await mediator.Send(requestModel);

            return Ok(response);
        }

        [HttpGet("details")]
        public async Task<IActionResult> OrderDetails([FromQuery] GetOrderByIdRequestModel requestModel)
        {
            var response = await mediator.Send(requestModel);

            return Ok(response);
        }
    }
}
