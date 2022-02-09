using System.Threading.Tasks;

namespace MyStore.Services.Products
{
    public interface IProductUpdate
    {
        Task UpdateAsync(ProductDTO dto);
    }
}
