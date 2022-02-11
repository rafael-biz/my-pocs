using System.Threading.Tasks;

namespace MyStore.Services.Products
{
    public interface IProductGetService
    {
        Task<ProductDetailsDTO> GetAsync(string sku);
    }
}
