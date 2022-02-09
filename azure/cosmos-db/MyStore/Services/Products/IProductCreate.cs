using System.Threading.Tasks;

namespace MyStore.Services.Products
{
    public interface IProductCreate
    {
        Task CreateAsync(ProductDTO dto);
    }
}
