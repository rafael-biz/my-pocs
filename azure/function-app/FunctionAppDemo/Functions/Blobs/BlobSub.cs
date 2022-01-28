using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionAppDemo.Functions.Blobs
{
    public static class BlobSub
    {
        [FunctionName("BlobSub")]
        public static void Run([BlobTrigger("blob-demo/{name}", Connection = "STORAGE_ACCOUNT_CONNECTION_STRING")] Stream data, string name, ILogger log)
        {
            log.LogInformation($"Blob name: { name }");
        }
    }
}
