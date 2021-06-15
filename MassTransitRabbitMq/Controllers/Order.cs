using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MassTransit;
using MassTransitRabbitMqShared;

namespace MassTransitRabbitMq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Order : ControllerBase
    {
        private readonly IPublishEndpoint _endpoint;

        public Order(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        [HttpPost]
        public async Task<IActionResult> MakeOrder(string productName, int amount)
        {
            await _endpoint.Publish<IOrderMessage>(new OrderSubmitted(productName, amount));

            return Ok("Order submitted");
        }
    }
}
