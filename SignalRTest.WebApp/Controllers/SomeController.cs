using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRTest.WebApp.Hubs;

namespace SignalRTest.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public SomeController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Get()
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "api controller", "hello from api controller");
            return Ok();
        }
    }
}
