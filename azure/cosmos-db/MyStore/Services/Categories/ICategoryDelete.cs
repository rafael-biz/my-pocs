using System.Threading.Tasks;

namespace MyStore.Services.Categories
{
    public interface ICategoryDelete
    {
        Task DeleteAsync(string sku);
    }
}
