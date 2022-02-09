using MyStore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Repositories.Categories
{
    public interface ICategoriesRepository
    {
        Task<List<Category>> GetAllAsync(string name = null);

        Task<Category> GetAsync(string id);

        Task CreateAsync(Category category);

        Task UpdateItemAsync(Category category);

        Task DeleteAsync(string id);
    }
}