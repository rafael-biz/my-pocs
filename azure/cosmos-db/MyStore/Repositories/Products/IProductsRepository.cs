using MyStore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Repositories.Products
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllAsync(string category = null);

        Task<Product> GetAsync(string sku);

        Task CreateAsync(Product product);

        Task UpdateItemAsync(Product product);

        Task DeleteAsync(string sku);
    }
}