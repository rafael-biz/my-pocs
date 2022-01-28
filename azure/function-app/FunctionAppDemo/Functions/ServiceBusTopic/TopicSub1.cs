using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionAppDemo.Functions.ServiceBusTopic
{
    public static class TopicSub1
    {
        [FunctionName("TopicSub1")]
        public static void Run([ServiceBusTrigger("topic_demo", "client_1", Connection = "CONNECTION_STRING_SB_TOPIC")] string message, ILogger log)
        {
            log.LogInformation($"Processed message from client_1: { message }");
        }
    }
}
