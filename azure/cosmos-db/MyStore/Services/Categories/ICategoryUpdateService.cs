using System.Threading.Tasks;

namespace MyStore.Services.Categories
{
    public interface ICategoryUpdateService
    {
        Task UpdateAsync(CategoryDTO dto);
    }
}
