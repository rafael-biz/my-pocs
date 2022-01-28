using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace FunctionAppDemo.Functions.ServiceBusTopic
{
    public class TopicPub
    {
        private readonly string connectionString;

        public TopicPub(IConfiguration configuration)
        {
            this.connectionString = configuration.GetValue<string>("CONNECTION_STRING_SB_TOPIC");
        }

        [FunctionName("TopicPub")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "topics/publish")] HttpRequest req)
        {
            await using (var client = new ServiceBusClient(connectionString))
            {
                var sender = client.CreateSender("topic_demo");

                await sender.SendMessageAsync(new ServiceBusMessage("Hello!"));
            }

            return new OkObjectResult("Messages published.");
        }
    }
}
