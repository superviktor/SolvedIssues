﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace VideoProcessor
{
    public static class HttpFunctions
    {
        [FunctionName(nameof(ProcessVideoStarter))]
        public static async Task<IActionResult> ProcessVideoStarter(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            var video = req.GetQueryParameterDictionary()["video"];
            if (video == null)
                return new BadRequestObjectResult("Please pass video url");
            
            string instanceId = await starter.StartNewAsync("ProcessVideoOrchestrator", null, video);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}