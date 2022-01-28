using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;

namespace FunctionAppDemo.Functions.Http
{
    public static class Greetings
    {
        [FunctionName("GreetingsGet")]
        public static async Task<IActionResult> Get([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "greetings")] HttpRequest req)
        {
            string name = req.Query["name"];

            string responseMessage = string.IsNullOrEmpty(name) ? "Hello, stranger." : $"Hello, { name }.";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("GreetingsPost")]
        public static async Task<IActionResult> Post([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "greetings")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            RequestData data = JsonConvert.DeserializeObject<RequestData>(requestBody);

            string name = data?.Name;

            var responseMessage = new RequestData()
            {
                Name = string.IsNullOrEmpty(name) ? "Hello, stranger." : $"Hello, { name }."
            };

            return new OkObjectResult(responseMessage);
        }
    }

    public class RequestData
    {
        public string Name { get; set; }
    }

    public class HelloMessage
    {
        public string Message { get; set; }
    }
}
