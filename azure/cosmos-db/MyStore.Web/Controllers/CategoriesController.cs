using Microsoft.AspNetCore.Mvc;
using MyStore.Services.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Web.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryGetService categoryGet;

        private readonly ICategoryListService categoryList;

        private readonly ICategoryCreateService categoryCreate;

        private readonly ICategoryUpdateService categoryUpdate;

        private readonly ICategoryDeleteService categoryDelete;

        public CategoriesController(
            ICategoryGetService categoryGet,
            ICategoryListService categoryList,
            ICategoryCreateService categoryCreate,
            ICategoryUpdateService categoryUpdate,
            ICategoryDeleteService categoryDelete)
        {
            this.categoryGet = categoryGet;
            this.categoryList = categoryList;
            this.categoryCreate = categoryCreate;
            this.categoryUpdate = categoryUpdate;
            this.categoryDelete = categoryDelete;
        }

        [HttpGet]
        public async Task<IList<CategoryDTO>> Get()
        {
            IList<CategoryDTO> list = await categoryList.GetListAsync();

            return list;
        }

        [HttpGet("{id}")]
        public async Task<CategoryDTO> GetAsync(string id)
        {
            CategoryDTO category = await categoryGet.GetAsync(id);

            return category;
        }

        [HttpPost]
        public async Task<CategoryDTO> Post([FromBody] CategoryDTO category)
        {
            return await categoryCreate.CreateAsync(category);
        }

        [HttpPut]
        public async Task Put([FromBody] CategoryDTO category)
        {
            await categoryUpdate.UpdateAsync(category);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await categoryDelete.DeleteAsync(id);
        }
    }
}
