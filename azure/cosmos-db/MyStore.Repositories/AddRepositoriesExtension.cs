using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyStore.Infrastructure;
using MyStore.Repositories.Categories;
using MyStore.Repositories.Products;

namespace MyStore.Repositories
{
    public static class AddRepositoriesExtension
    {
        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            CosmosSettings settings = new CosmosSettings()
            {
                EndpointUri = configuration["EndPointUri"],
                PrimaryKey = configuration["PrimaryKey"],
                DatabaseId = configuration["DatabaseId"],
                ApplicationName = configuration["ApplicationName"]
            };

            services.AddSingleton<IProductsSettings>(settings);

            services.AddSingleton<DatabaseFactory>();

            services.AddHostedService(provider => provider.GetService<DatabaseFactory>());

            services.AddSingleton<ContainerFactory>();

            services.AddHostedService(provider => provider.GetService<ContainerFactory>());

            services.AddTransient<IProductsRepository, ProductsRepository>();

            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
        }
    }
}
