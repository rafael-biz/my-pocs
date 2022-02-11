using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Services.Categories
{
    public interface ICategoryListService
    {
        Task<IList<CategoryDTO>> GetListAsync();
    }
}
