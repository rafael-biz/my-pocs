using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using MyStore.Entities;
using MyStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Repositories.Products
{
    public sealed class ProductsRepository : IProductsRepository
    {
        private readonly ILogger<ProductsRepository> logger;

        private readonly Container container;

        public ProductsRepository(ILogger<ProductsRepository> logger, ContainerFactory containerFactory)
        {
            this.logger = logger;
            this.container = containerFactory.GetProductsContainer();
        }

        public async Task<Product> GetAsync(string sku)
        {
            try
            {
                ItemResponse<Product> response = await this.container.ReadItemAsync<Product>(sku, new PartitionKey(sku));

                logger.LogInformation("Read operation consumed {0} RUs.", response.RequestCharge);

                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new EntityNotFoundException(ex);
            }
        }

        public async Task CreateAsync(Product product)
        {
            try
            {
                ProductDocument document = new ProductDocument()
                {
                    Sku = product.Sku,
                    PartitionKey = product.Sku,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Tags = product.Tags,
                    CategoryId = product.Category.Id,
                    CategoryName = product.Category.Name
                };

                ItemResponse<ProductDocument> response = await container.CreateItemAsync(document, new PartitionKey(product.Sku));

                logger.LogInformation("Create operation consumed {0} RUs.", response.RequestCharge);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                throw new EntityConflictException(ex);
            }
        }

        public async Task UpdateItemAsync(Product product)
        {
            var response = await this.container.ReplaceItemAsync<Product>(product, product.Sku, new PartitionKey(product.Sku));

            logger.LogInformation("Updated operation consumed {0} RUs.", response.RequestCharge);
        }

        public async Task<List<Product>> GetAllAsync(string category = null)
        {
            var sql = "SELECT * FROM c WHERE @category = null OR c.Category = @category";

            QueryDefinition query = new QueryDefinition(sql)
                .WithParameter("@category", category);

            using FeedIterator<ProductDocument> iterator = container.GetItemQueryIterator<ProductDocument>(query);

            List<Product> products = new List<Product>();

            while (iterator.HasMoreResults)
            {
                FeedResponse<ProductDocument> response = await iterator.ReadNextAsync();

                foreach (ProductDocument item in response)
                {
                    products.Add(new Product() {
                        Sku = item.Sku,
                        Name = item.Name,
                        Description = item.Description,
                        Price = item.Price,
                        Tags = item.Tags,
                        Category = new Category()
                        {
                            Id = item.CategoryId,
                            Name = item.CategoryName
                        }
                    });
                }

                logger.LogInformation("Query consumed {0} RUs.", response.RequestCharge);
            }

            return products;
        }

        public async Task DeleteAsync(string sku)
        {
            ItemResponse<Product> response = await this.container.DeleteItemAsync<Product>(sku, new PartitionKey(sku));

            logger.LogInformation("Delete operation consumed {0} RUs.", response.RequestCharge);
        }
    }
}
