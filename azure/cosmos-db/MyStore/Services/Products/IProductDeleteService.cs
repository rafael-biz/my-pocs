using System.Threading.Tasks;

namespace MyStore.Services.Products
{
    public interface IProductDeleteService
    {
        Task DeleteAsync(string sku);
    }
}
