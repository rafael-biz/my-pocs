using System.Threading.Tasks;

namespace MyStore.Services.Categories
{
    public interface ICategoryDeleteService
    {
        Task DeleteAsync(string sku);
    }
}
