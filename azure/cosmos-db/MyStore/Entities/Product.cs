namespace MyStore.Entities
{
    public class Product
    {
        public string Sku { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string[] Tags { get; set; }
    }
}
