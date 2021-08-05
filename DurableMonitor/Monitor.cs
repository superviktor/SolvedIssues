using System.Threading;
using System.Threading.Tasks;
using DurableMonitor.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableMonitor
{
    public class Monitor
    {
        [FunctionName("Monitor")]
        public async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            var input = context.GetInput<MonitorRequest>();
            if (!context.IsReplaying) { log.LogInformation($"Received monitor request. Location: {input?.Location}."); }

            var endTime = context.CurrentUtcDateTime.AddMinutes(5);
            if (!context.IsReplaying) { log.LogInformation($"Instantiating monitor for {input.Location}. Expires: {endTime}."); }

            while (context.CurrentUtcDateTime < endTime)
            {
                if (!context.IsReplaying)
                    if (!context.IsReplaying) { log.LogInformation($"Checking current weather conditions for {input.Location} at {context.CurrentUtcDateTime}."); }

                var isSnowing = await context.CallActivityAsync<bool>("IsSnowing", input.Location);
                if (isSnowing)
                {
                    if (!context.IsReplaying)
                        log.LogInformation($"Detected clear weather for {input.Location}. Notify {input.Phone}");

                    await context.CallActivityAsync("Notify", input.Phone);
                    break;
                }

                var nextCheckPoint = context.CurrentUtcDateTime.AddSeconds(10);
                if (!context.IsReplaying)
                    log.LogInformation($"Next check for {input.Location} at {nextCheckPoint}.");
                await context.CreateTimer(nextCheckPoint, CancellationToken.None);

                log.LogInformation("Monitor expiring.");
            }
        }
    }
}