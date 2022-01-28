using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FunctionAppDemo.Functions.Blobs
{
    public class BlobPub
    {
        private readonly BlobServiceClient blobServiceClient;

        public BlobPub(IConfiguration configuration)
        {
            string connectionString = configuration.GetValue<string>("STORAGE_ACCOUNT_CONNECTION_STRING");

            this.blobServiceClient = new BlobServiceClient(connectionString);
        }

        [FunctionName("BlobPub")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "blob/publish")] HttpRequest req)
        {
            BlobContainerClient client = blobServiceClient.GetBlobContainerClient("blob-demo");

            BlobClient blobClient = client.GetBlobClient(Guid.NewGuid().ToString("D") + ".txt");

            await blobClient.UploadAsync(new MemoryStream(0));

            return new OkObjectResult("Ok!");
        }
    }
}
