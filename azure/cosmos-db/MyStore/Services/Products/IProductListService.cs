using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Services.Products
{
    public interface IProductListService
    {
        Task<IList<ProductDetailsDTO>> GetListAsync();
    }
}
