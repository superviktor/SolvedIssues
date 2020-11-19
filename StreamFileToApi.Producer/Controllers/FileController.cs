using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StreamFileToApi.Producer.Controllers
{
    [ApiController]
    public class FileController : Controller
    {
        /// <summary>
        /// Upload file
        /// </summary>
        [HttpPost]
        [Route("/api/files/upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null)
                return BadRequest("File is not selected");

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5003/api/files/receive"))
            await using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);
                var httpContent = new StreamContent(stream);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Content = httpContent;

                using var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                response.EnsureSuccessStatusCode();
            }

            return Ok();
        }
    }
}
