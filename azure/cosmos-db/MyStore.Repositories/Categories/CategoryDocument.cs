using Newtonsoft.Json;

namespace MyStore.Repositories.Categories
{
    public sealed class CategoryDocument
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
