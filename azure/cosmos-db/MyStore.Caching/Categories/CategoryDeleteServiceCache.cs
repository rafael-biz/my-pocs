using MyStore.Services.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Caching.Categories
{
    public sealed class CategoryDeleteServiceCache : ICategoryDeleteService
    {
        private readonly RedisClient redisClient;

        private readonly ICategoryDeleteService deleteService;

        public CategoryDeleteServiceCache(RedisClient redisClient, ICategoryDeleteService deleteService)
        {
            this.redisClient = redisClient;
            this.deleteService = deleteService;
        }

        public async Task DeleteAsync(string sku)
        {
            try
            {
                await deleteService.DeleteAsync(sku);
            }
            finally
            {
                await redisClient.Invalidade("categories");
                await redisClient.Invalidade("categories:" + sku);
            }
        }
    }
}
