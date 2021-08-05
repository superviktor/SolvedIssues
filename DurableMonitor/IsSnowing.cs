using System;
using System.Threading.Tasks;
using DurableMonitor.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableMonitor
{
    public class IsSnowing
    {
        [FunctionName("IsSnowing")]
        public Task<bool> Run([ActivityTrigger] Location location, ILogger log)
        {
            log.LogInformation("Weather api call");
            var random = new Random();
            var number = random.Next(1, 100);
            return Task.FromResult(number % 2 == 0);
        }
    }
}
