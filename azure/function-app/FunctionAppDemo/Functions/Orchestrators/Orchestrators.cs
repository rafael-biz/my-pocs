using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionAppDemo.Functions.Orchestrators
{
    public static class Orchestrators
    {
        [FunctionName("Orchestrator")]
        public static async Task<List<string>> RunOrchestrator([OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            outputs.Add(await context.CallActivityAsync<string>("Orchestrator_Hello", "Tokyo"));
            outputs.Add(await context.CallActivityAsync<string>("Orchestrator_Hello", "Seattle"));
            outputs.Add(await context.CallActivityAsync<string>("Orchestrator_Hello", "London"));

            return outputs;
        }

        [FunctionName("Orchestrator_Hello")]
        public static string SayHello([ActivityTrigger] string name, ILogger log)
        {
            string message = $"Hello {name}!";

            log.LogInformation(message);

            return message;
        }

        [FunctionName("Orchestrator_Start")]
        public static async Task<string> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "orchestrator")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter)
        {
            string instanceId = await starter.StartNewAsync("Orchestrator", null);

            return instanceId;
        }

        [FunctionName("Orchestrator_Status")]
        public static async Task<string> GetStatus(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "orchestrator/{instanceId}/status")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            string instanceId)
        {
            var status = await starter.GetStatusAsync(instanceId);

            return status.RuntimeStatus.ToString();
        }

        [FunctionName("Orchestrator_Output")]
        public static async Task<string> GetOutput(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "orchestrator/{instanceId}/output")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            string instanceId)
        {
            var status = await starter.GetStatusAsync(instanceId);

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return status.Output.ToString();
        }
    }
}