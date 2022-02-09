using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Services.Categories
{
    public interface ICategoryList
    {
        Task<IList<CategoryDTO>> GetListAsync();
    }
}
