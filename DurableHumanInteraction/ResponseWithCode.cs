using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Text.Json;

namespace DurableHumanInteraction
{
    public static class ResponseWithCode
    {
        [FunctionName("ResponseWithCode")]
        public static async Task Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log, [DurableClient] IDurableOrchestrationClient client)
        {
            var reader = new StreamReader(req.Body);
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            var rawMessage = await reader.ReadToEndAsync();
            var input = JsonSerializer.Deserialize<ResponseWithCodeInput>(rawMessage, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            await client.RaiseEventAsync(input.InstanceId, "SmsChallengeResponse", input.Code);         }
    }
}
