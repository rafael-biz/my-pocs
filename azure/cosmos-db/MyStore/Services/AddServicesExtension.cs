using Microsoft.Extensions.DependencyInjection;
using MyStore.Services.Categories;
using MyStore.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Services
{
    public static class AddServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IProductGetService, ProductsService>();

            services.AddTransient<IProductListService, ProductsService>();

            services.AddTransient<IProductCreateService, ProductsService>();

            services.AddTransient<IProductUpdateService, ProductsService>();

            services.AddTransient<IProductDeleteService, ProductsService>();

            services.AddTransient<ICategoryGetService, CategoriesService>();

            services.AddTransient<ICategoryListService, CategoriesService>();

            services.AddTransient<ICategoryCreateService, CategoriesService>();

            services.AddTransient<ICategoryUpdateService, CategoriesService>();

            services.AddTransient<ICategoryDeleteService, CategoriesService>();
        }
    }
}
