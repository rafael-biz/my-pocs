using System.Threading.Tasks;

namespace MyStore.Services.Categories
{
    public interface ICategoryCreateService
    {
        Task<CategoryDTO> CreateAsync(CategoryDTO dto);
    }
}
