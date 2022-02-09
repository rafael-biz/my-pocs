namespace MyStore.Services.Products
{
    public sealed class ProductDetailsDTO
    {
        public string Sku { get; set; }

        public string Name { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string[] Tags { get; set; }
    }
}
