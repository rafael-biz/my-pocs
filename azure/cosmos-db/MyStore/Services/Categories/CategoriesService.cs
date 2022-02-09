using MyStore.Entities;
using MyStore.Repositories.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services.Categories
{
    public sealed class CategoriesService : ICategoryGet, ICategoryList, ICategoryCreate, ICategoryUpdate, ICategoryDelete
    {
        private readonly ICategoriesRepository categories;

        public CategoriesService(ICategoriesRepository categories)
        {
            this.categories = categories;
        }

        public async Task<CategoryDTO> GetAsync(string id)
        {
            Category category = await categories.GetAsync(id);

            CategoryDTO dto = new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name
            };

            return dto;
        }

        public async Task<IList<CategoryDTO>> GetListAsync()
        {
            IList<Category> list = await categories.GetAllAsync();

            return list.Select(dto => new CategoryDTO()
            {
                Id = dto.Id,
                Name = dto.Name
            }).ToList();
        }

        public async Task<CategoryDTO> CreateAsync(CategoryDTO dto)
        {
            Category category = new Category()
            {
                Name = dto.Name
            };

            await categories.CreateAsync(category);

            dto.Id = category.Id;

            return dto;
        }

        public async Task UpdateAsync(CategoryDTO dto)
        {
            Category category = new Category()
            {
                Id = dto.Id,
                Name = dto.Name
            };

            await categories.UpdateItemAsync(category);
        }

        public async Task DeleteAsync(string id)
        {
            await categories.DeleteAsync(id);
        }
    }
}
