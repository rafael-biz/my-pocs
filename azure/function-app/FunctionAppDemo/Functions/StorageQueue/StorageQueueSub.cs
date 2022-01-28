using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Queue;

namespace FunctionAppDemo.Functions.StorageQueue
{
    public static class StorageQueueSub
    {
        [FunctionName("StorageQueueSub")]
        public static void Run([QueueTrigger("queue-demo", Connection = "STORAGE_ACCOUNT_CONNECTION_STRING")] CloudQueueMessage message, ILogger log)
        {
            string body = message.AsString;

            log.LogInformation($"Message received: { body }");
        }
    }
}