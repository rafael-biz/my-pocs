using System.Threading.Tasks;

namespace MyStore.Services.Products
{
    public interface IProductDelete
    {
        Task DeleteAsync(string sku);
    }
}
