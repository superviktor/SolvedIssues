using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace DurableFanOutInt
{
    public static class OrchestratorFunction
    {
        /// <summary>
        /// Receives directory name, upload all files from dir to blob and returns amount of uploaded bytes
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [FunctionName("BackupSiteContent")]
        public static async Task<long> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var directory = context.GetInput<string>()?.Trim();
            if (string.IsNullOrWhiteSpace(directory))
                directory = Directory.GetParent(typeof(OrchestratorFunction).Assembly.Location).FullName;
            var files = await context.CallActivityAsync<string[]>("GetFileList", directory);
            var tasks = new Task<long>[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                tasks[i] = context.CallActivityAsync<long>("CopyFileToBlob", files[i]);
            }

            await Task.WhenAll(tasks);
            var totalBytes = tasks.Sum(t => t.Result);
            return totalBytes;
        }
    }
}