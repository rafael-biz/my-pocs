using MyStore.Services.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Caching.Categories
{
    public sealed class CategoryListServiceCache : ICategoryListService
    {
        private readonly RedisClient redisClient;

        private readonly ICategoryListService listService;

        public CategoryListServiceCache(RedisClient redisClient, ICategoryListService listService)
        {
            this.redisClient = redisClient;
            this.listService = listService;
        }

        public Task<IList<CategoryDTO>> GetListAsync()
        {
            return redisClient.GetJsonAsync("categories", async () => await listService.GetListAsync());
        }
    }
}
