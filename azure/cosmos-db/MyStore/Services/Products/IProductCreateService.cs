using System.Threading.Tasks;

namespace MyStore.Services.Products
{
    public interface IProductCreateService
    {
        Task CreateAsync(ProductDTO dto);
    }
}
