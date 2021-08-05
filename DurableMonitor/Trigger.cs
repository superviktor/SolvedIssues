using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using DurableMonitor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace DurableMonitor
{
    public static class Trigger
    {
        [FunctionName(nameof(Trigger))]
        public static async Task<IActionResult> Start(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            var reader = new StreamReader(req.Body);
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            var rawMessage = await reader.ReadToEndAsync();
            var monitorRequest = JsonSerializer.Deserialize<MonitorRequest>(rawMessage, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            var instanceId = await starter.StartNewAsync("Monitor", null, monitorRequest);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}