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
            services.AddTransient<IProductGet, ProductsService>();

            services.AddTransient<IProductList, ProductsService>();

            services.AddTransient<IProductCreate, ProductsService>();

            services.AddTransient<IProductUpdate, ProductsService>();

            services.AddTransient<IProductDelete, ProductsService>();

            services.AddTransient<ICategoryGet, CategoriesService>();

            services.AddTransient<ICategoryList, CategoriesService>();

            services.AddTransient<ICategoryCreate, CategoriesService>();

            services.AddTransient<ICategoryUpdate, CategoriesService>();

            services.AddTransient<ICategoryDelete, CategoriesService>();
        }
    }
}
