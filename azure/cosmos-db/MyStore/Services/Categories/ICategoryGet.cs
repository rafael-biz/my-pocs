using System.Threading.Tasks;

namespace MyStore.Services.Categories
{
    public interface ICategoryGet
    {
        Task<CategoryDTO> GetAsync(string sky);
    }
}
