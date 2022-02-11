using MyStore.Services.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Caching.Categories
{
    public sealed class CategoryCreateServiceCache : ICategoryCreateService
    {
        private readonly RedisClient redisClient;

        private readonly ICategoryCreateService createService;

        public CategoryCreateServiceCache(RedisClient redisClient, ICategoryCreateService createService)
        {
            this.redisClient = redisClient;
            this.createService = createService;
        }

        public async Task<CategoryDTO> CreateAsync(CategoryDTO dto)
        {
            try
            {
                return await createService.CreateAsync(dto);
            }
            finally
            {
                await redisClient.Invalidade("categories");
            }
        }
    }
}
