using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;

namespace FunctionAppDemo.Functions.StorageQueue
{
    public class StorageQueuePub
    {
        private readonly QueueClient queueClient;

        public StorageQueuePub(IConfiguration configuration)
        {
            string connectionString = configuration.GetValue<string>("STORAGE_ACCOUNT_CONNECTION_STRING");

            this.queueClient = new QueueClient(connectionString, "queue-demo", new QueueClientOptions()
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });
        }

        [FunctionName("StorageQueuePub")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "sa-queue/publish")] HttpRequest req)
        {
            await queueClient.SendMessageAsync("Hello!");

            return new OkObjectResult("Message published.");
        }
    }
}
