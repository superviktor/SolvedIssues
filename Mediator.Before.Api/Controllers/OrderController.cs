using Microsoft.AspNetCore.Mvc;

namespace Mediator.Before.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMakeOrderCommandHandler makeOrderCommandHandler;
        private readonly IGetOrderByIdQueryHandler getOrderByIdQueryHandler;

        public OrderController(IMakeOrderCommandHandler makeOrderCommandHandler, IGetOrderByIdQueryHandler getOrderByIdQueryHandler)
        {
            this.makeOrderCommandHandler = makeOrderCommandHandler;
            this.getOrderByIdQueryHandler = getOrderByIdQueryHandler;
        }

        [HttpPost("makeorder")]
        public IActionResult MakeOrder([FromBody] MakeOrderRequestModel requestModel)
        {
            var response = makeOrderCommandHandler.MakeOrder(requestModel);

            return Ok(response);
        }

        [HttpGet("details")]
        public IActionResult OrderDetails([FromQuery] GetOrderByIdRequestModel requestModel)
        {
            var response = getOrderByIdQueryHandler.MakeOrder(requestModel);

            return Ok(response);
        }
    }
}
