using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StreamFileToApi.Consumer.Controllers
{
    [ApiController]
    public class FIleController : Controller
    {
        [HttpPost]
        [Route("/api/files/receive")]
        public async Task<IActionResult> Post()
        {
            var stream = Request.Body;
            using StreamReader reader = new StreamReader(stream);
            string text = await reader.ReadToEndAsync();
            return Ok(text);
        }
    }
}
