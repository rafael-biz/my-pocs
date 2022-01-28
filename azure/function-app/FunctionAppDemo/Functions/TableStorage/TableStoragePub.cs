using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FunctionAppDemo.Functions.TableStorage
{
    public class TableStoragePub
    {
        private readonly TableServiceClient tableServiceClient;

        public TableStoragePub(IConfiguration configuration)
        {
            string connectionString = configuration.GetValue<string>("STORAGE_ACCOUNT_CONNECTION_STRING");

            this.tableServiceClient = new TableServiceClient(connectionString);
        }

        [FunctionName("TablePost")]
        public async Task<IActionResult> Post([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "storage-table")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            CarDTO car = JsonConvert.DeserializeObject<CarDTO>(requestBody);

            TableClient tableClient = tableServiceClient.GetTableClient("cars");

            string guid = Guid.NewGuid().ToString();

            var entity = new TableEntity("main", guid)
            {
                { "Model", car.Model },
                { "Price", car.Price },
            };

            await tableClient.AddEntityAsync(entity);

            return new OkObjectResult("Messages published: " + guid);
        }

        [FunctionName("TableList")]
        public async Task<IActionResult> GetList([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "storage-table")] HttpRequest req)
        {
            TableClient tableClient = tableServiceClient.GetTableClient("cars");

            var query = tableClient.QueryAsync<CarEntity>();

            List<CarDTO> cars = new List<CarDTO>();

            await foreach (CarEntity car in query)
            {
                cars.Add(new CarDTO()
                {
                    Id = car.RowKey,
                    Model = car.Model,
                    Price = car.Price
                });
            }

            return new OkObjectResult(cars);
        }

        [FunctionName("TableGet")]
        public async Task<IActionResult> GetById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "storage-table/{guid}")] HttpRequest req, string guid)
        {
            TableClient tableClient = tableServiceClient.GetTableClient("cars");

            CarEntity car = tableClient.GetEntity<CarEntity>("main", guid);

            var dto = new CarDTO()
            {
                Id = car.RowKey,
                Model = car.Model,
                Price = car.Price
            };

            return new OkObjectResult(dto);
        }

        [FunctionName("TablePut")]
        public async Task<IActionResult> Put([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "storage-table/{guid}")] HttpRequest req, string guid)
        {
            TableClient tableClient = tableServiceClient.GetTableClient("cars");

            CarEntity car = tableClient.GetEntity<CarEntity>("main", guid);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            CarDTO dto = JsonConvert.DeserializeObject<CarDTO>(requestBody);

            car.Model = dto.Model;
            car.Price = dto.Price;

            await tableClient.UpdateEntityAsync(car, car.ETag);

            return new OkObjectResult(dto);
        }

        [FunctionName("TableDelete")]
        public async Task<IActionResult> Delete([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "storage-table/{guid}")] HttpRequest req, string guid)
        {
            TableClient tableClient = tableServiceClient.GetTableClient("cars");

            await tableClient.DeleteEntityAsync("main", guid);

            return new OkObjectResult("Deleted!");
        }
    }

    public class CarDTO
    {
        public string Id { get; set; }

        public string Model { get; set; }

        public double Price { get; set; }
    }

    public class CarEntity : ITableEntity
    {
        public string Model { get; set; }

        public double Price { get; set; }

        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }
    }
}
