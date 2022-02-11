using MyStore.Services.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Caching.Categories
{
    public sealed class CategoryUpdateServiceCache : ICategoryUpdateService
    {
        private readonly RedisClient redisClient;

        private readonly ICategoryUpdateService updateService;

        public CategoryUpdateServiceCache(RedisClient redisClient, ICategoryUpdateService updateService)
        {
            this.redisClient = redisClient;
            this.updateService = updateService;
        }

        public async Task UpdateAsync(CategoryDTO dto)
        {
            try
            {
                await updateService.UpdateAsync(dto);
            }
            finally
            {
                await redisClient.Invalidade("categories");
                await redisClient.Invalidade("categories:" + dto.Id);
            }
        }
    }
}
