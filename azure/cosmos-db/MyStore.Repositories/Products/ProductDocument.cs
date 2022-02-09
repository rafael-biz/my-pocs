using Newtonsoft.Json;

namespace MyStore.Repositories.Products
{
    public class ProductDocument
    {
        [JsonProperty(PropertyName = "id")]
        public string Sku { get; set; }

        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }

        public string Name { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string[] Tags { get; set; }
    }
}
