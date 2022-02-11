using System.Threading.Tasks;

namespace MyStore.Services.Products
{
    public interface IProductUpdateService
    {
        Task UpdateAsync(ProductDTO dto);
    }
}
