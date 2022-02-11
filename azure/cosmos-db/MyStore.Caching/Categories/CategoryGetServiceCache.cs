using MyStore.Services.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Caching.Categories
{
    public sealed class CategoryGetServiceCache : ICategoryGetService
    {
        private readonly RedisClient redisClient;

        private readonly ICategoryGetService getService;

        public CategoryGetServiceCache(RedisClient redisClient, ICategoryGetService getService)
        {
            this.redisClient = redisClient;
            this.getService = getService;
        }

        public Task<CategoryDTO> GetAsync(string sku)
        {
            return redisClient.GetJsonAsync("categories:" + sku, async () => await getService.GetAsync(sku));
        }
    }
}
