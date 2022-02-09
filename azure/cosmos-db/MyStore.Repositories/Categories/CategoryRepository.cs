using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using MyStore.Entities;
using MyStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using static MyStore.Repositories.Globals;

namespace MyStore.Repositories.Categories
{
    public sealed class CategoriesRepository: ICategoriesRepository
    {
        private readonly ILogger<CategoriesRepository> logger;

        private readonly Container container;

        public CategoriesRepository(ILogger<CategoriesRepository> logger, ContainerFactory containerFactory)
        {
            this.logger = logger;
            this.container = containerFactory.GetCategoriesContainer();
        }

        public async Task<Category> GetAsync(string id)
        {
            try
            {
                ItemResponse<CategoryDocument> response = await this.container.ReadItemAsync<CategoryDocument>(id, new PartitionKey(id));

                logger.LogInformation("Read operation consumed {0} RUs.", response.RequestCharge);

                CategoryDocument document = response.Resource;

                return new Category()
                {
                    Id = document.Id,
                    Name = document.Name
                };
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new EntityNotFoundException(ex);
            }
        }

        public async Task CreateAsync(Category entity)
        {
            try
            {
                entity.Id = Guid.NewGuid().ToString();

                CategoryDocument document = new CategoryDocument()
                {
                    Id = entity.Id,
                    PartitionKey = entity.Id,
                    Name = entity.Name
                };

                ItemResponse<CategoryDocument> response = await container.CreateItemAsync(document, new PartitionKey(entity.Id), DisableContentResponseOnWrite);

                logger.LogInformation("Create operation consumed {0} RUs.", response.RequestCharge);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                throw new EntityConflictException(ex);
            }
        }

        public async Task UpdateItemAsync(Category entity)
        {
            CategoryDocument document = new CategoryDocument()
            {
                Id = entity.Id,
                PartitionKey = entity.Id,
                Name = entity.Name
            };

            var response = await this.container.ReplaceItemAsync<CategoryDocument>(document, entity.Id, new PartitionKey(entity.Id));

            logger.LogInformation("Updated operation consumed {0} RUs.", response.RequestCharge);
        }

        public async Task<List<Category>> GetAllAsync(string name = null)
        {
            var sql = "SELECT * FROM c WHERE @name = null OR c.Name = @name";

            QueryDefinition query = new QueryDefinition(sql)
                .WithParameter("@name", name);

            using FeedIterator<CategoryDocument> iterator = container.GetItemQueryIterator<CategoryDocument>(query);

            List<Category> list = new List<Category>();

            while (iterator.HasMoreResults)
            {
                FeedResponse<CategoryDocument> response = await iterator.ReadNextAsync();

                foreach (CategoryDocument document in response)
                {
                    list.Add(new Category()
                    {
                        Id = document.Id,
                        Name = document.Name
                    });
                }

                logger.LogInformation("Query consumed {0} RUs.", response.RequestCharge);
            }

            return list;
        }

        public async Task DeleteAsync(string id)
        {
            ItemResponse<Category> response = await this.container.DeleteItemAsync<Category>(id, new PartitionKey(id));

            logger.LogInformation("Delete operation consumed {0} RUs.", response.RequestCharge);
        }
    }
}
