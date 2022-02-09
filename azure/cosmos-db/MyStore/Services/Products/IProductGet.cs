using System.Threading.Tasks;

namespace MyStore.Services.Products
{
    public interface IProductGet
    {
        Task<ProductDetailsDTO> GetAsync(string sky);
    }
}
