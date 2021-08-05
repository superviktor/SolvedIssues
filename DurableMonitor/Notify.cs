using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableMonitor
{
    public class Notify
    {
        [FunctionName("Notify")]
        public Task Run([ActivityTrigger] string phone, ILogger log)
        {
            log.LogInformation("Notification api call");
            return Task.CompletedTask;
        }
    }
}
