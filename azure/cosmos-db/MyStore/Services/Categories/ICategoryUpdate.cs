using System.Threading.Tasks;

namespace MyStore.Services.Categories
{
    public interface ICategoryUpdate
    {
        Task UpdateAsync(CategoryDTO dto);
    }
}
