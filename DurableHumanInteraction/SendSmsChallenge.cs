using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableHumanInteraction
{
    public static class SendSmsChallenge
    {
        [FunctionName(nameof(SendSmsChallenge))]
        public static Task<int> Run([ActivityTrigger] string phoneNumber, ILogger log)
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
            int challengeCode = rand.Next(10000);

            log.LogInformation($"Sending verification code {challengeCode} to {phoneNumber}.");

            return Task.FromResult(challengeCode);
        }
    }
}
