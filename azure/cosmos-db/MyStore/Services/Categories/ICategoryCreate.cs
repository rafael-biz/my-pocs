using System.Threading.Tasks;

namespace MyStore.Services.Categories
{
    public interface ICategoryCreate
    {
        Task<CategoryDTO> CreateAsync(CategoryDTO dto);
    }
}
