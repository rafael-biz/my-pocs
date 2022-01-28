using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionAppDemo.Functions.ServiceBusQueue
{
    public static class QueueSub
    {
        [FunctionName("QueueSub")]
        public static async Task Run([ServiceBusTrigger("queue_demo", Connection = "CONNECTION_STRING_SB_QUEUE")] string message, ILogger log)
        {
            log.LogInformation($"Processed message: { message }");

            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }
}
