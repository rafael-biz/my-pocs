using Microsoft.AspNetCore.Mvc;
using MyStore.Entities;
using MyStore.Repositories.Categories;
using MyStore.Services.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Web.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryGet categoryGet;

        private readonly ICategoryList categoryList;

        private readonly ICategoryCreate categoryCreate;

        private readonly ICategoryUpdate categoryUpdate;

        private readonly ICategoryDelete categoryDelete;

        public CategoriesController(
            ICategoryGet categoryGet,
            ICategoryList categoryList,
            ICategoryCreate categoryCreate,
            ICategoryUpdate categoryUpdate,
            ICategoryDelete categoryDelete)
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
