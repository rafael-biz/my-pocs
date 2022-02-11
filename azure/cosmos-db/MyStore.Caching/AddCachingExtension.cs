using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyStore.Caching.Categories;
using MyStore.Services.Categories;
using StackExchange.Redis;

namespace MyStore.Caching
{
    public static class AddCachingExtension
    {
        public static void AddCaching(this IServiceCollection services, IConfiguration configuration)
        {
            RedisSettings settings = new RedisSettings()
            {
                ConnectionString = configuration["ConnectionString"]
            };

            services.AddSingleton<IRedisSettings>(settings);

            IConnectionMultiplexer redis = ConnectionMultiplexer.Connect(settings.ConnectionString);

            services.AddSingleton(redis);

            services.AddSingleton<RedisClient>();

            services.Decorate<ICategoryGetService, CategoryGetServiceCache>();

            services.Decorate<ICategoryListService, CategoryListServiceCache>();

            services.Decorate<ICategoryCreateService, CategoryCreateServiceCache>();

            services.Decorate<ICategoryUpdateService, CategoryUpdateServiceCache>();

            services.Decorate<ICategoryDeleteService, CategoryDeleteServiceCache>();
        }
    }
}
