using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace VideoProcessor
{
    public class OrchestratorFunctions
    {
        [FunctionName(nameof(ProcessVideoOrchestrator))]
        public static async Task<object> ProcessVideoOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            ILogger log)
        {
            log = context.CreateReplaySafeLogger(log);

            var videoLocation = context.GetInput<string>();
            log.LogInformation("Orchestrator call TranscodeVideo");
            var transcodedLocation = await context.CallActivityAsync<string>("TranscodeVideo", videoLocation);
            log.LogInformation("Orchestrator call ExtractThumbnail");
            var thumbnailLocation  = await context.CallActivityAsync<string>("ExtractThumbnail", transcodedLocation);
            log.LogInformation("Orchestrator call PrependIntro");
            var withIntroLocation  = await context.CallActivityAsync<string>("PrependIntro", thumbnailLocation);

            return new
            {
                transcodedLocation,
                thumbnailLocation,
                withIntroLocation
            };
        }
    }
}