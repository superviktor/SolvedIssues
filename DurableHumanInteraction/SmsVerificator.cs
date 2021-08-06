using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DurableHumanInteraction
{
    public static class SmsVerificator
    {
        [FunctionName(nameof(SmsVerificator))]
        public static async Task<bool> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            var phone = context.GetInput<string>();
            if (string.IsNullOrEmpty(phone))
                throw new ArgumentNullException("Phone number is required");

            var challengeCode = await context.CallActivityAsync<int>("SendSmsChallenge", phone);
            using (var timoutCts = new CancellationTokenSource())
            {
                var expiration = context.CurrentUtcDateTime.AddSeconds(90);
                var timeoutTask = context.CreateTimer(expiration, timoutCts.Token);
                var authorized = false;
                for (int retryCount = 0; retryCount < 3; retryCount++)
                {
                    var challengeResposeTask = context.WaitForExternalEvent<int>("SmsChallengeResponse");
                    var winner = await Task.WhenAny(challengeResposeTask, timeoutTask);
                    if (winner == challengeResposeTask)
                    {
                        if (challengeResposeTask.Result == challengeCode)
                        {
                            authorized = true;
                            log.LogInformation("Authorization success");
                            break;
                        }
                    }
                    else
                    {
                        log.LogInformation("Authorization failure");
                        break;
                    }
                }

                if (!timeoutTask.IsCompleted)
                    timoutCts.Cancel();

                return authorized;
            }
        }
    }
}