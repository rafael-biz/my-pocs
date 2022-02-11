using System.Threading.Tasks;

namespace MyStore.Services.Categories
{
    public interface ICategoryGetService
    {
        Task<CategoryDTO> GetAsync(string sku);
    }
}
