using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace FunctionAppDemo.Functions.ServiceBusQueue
{
    public class QueuePub
    {
        private readonly string connectionString;

        public QueuePub(IConfiguration configuration)
        {
            this.connectionString = configuration.GetValue<string>("CONNECTION_STRING_SB_QUEUE");
        }

        [FunctionName("QueuePub")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "queue/publish")] HttpRequest req)
        {
            await using (var client = new ServiceBusClient(connectionString))
            {
                var sender = client.CreateSender("queue_demo");

                int count = 100;

                int.TryParse(req.Query["count"], out count);

                for (int i = 1; i <= count; i++)
                {
                    await sender.SendMessageAsync(new ServiceBusMessage("Test: " + i));
                }
            }

            return new OkObjectResult("Messages published.");
        }
    }
}
