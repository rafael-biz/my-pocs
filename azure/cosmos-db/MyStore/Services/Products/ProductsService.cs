using MyStore.Entities;
using MyStore.Repositories.Categories;
using MyStore.Repositories.Products;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services.Products
{
    public sealed class ProductsService : IProductGet, IProductList, IProductCreate, IProductUpdate, IProductDelete
    {
        private readonly IProductsRepository products;

        private readonly ICategoriesRepository categories;

        public ProductsService(IProductsRepository products, ICategoriesRepository categories)
        {
            this.products = products;
            this.categories = categories;
        }

        public async Task<ProductDetailsDTO> GetAsync(string sku)
        {
            Product product = await products.GetAsync(sku);

            ProductDetailsDTO dto = new ProductDetailsDTO()
            {
                Sku = product.Sku,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.Category.Id,
                CategoryName = product.Category.Name,
                Tags = product.Tags
            };

            return dto;
        }

        public async Task<IList<ProductDetailsDTO>> GetListAsync()
        {
            IList<Product> list = await products.GetAllAsync();

            return list.Select(dto => new ProductDetailsDTO()
            {
                Sku = dto.Sku,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.Category?.Id,
                CategoryName = dto.Category?.Name,
                Tags = dto.Tags
            }).ToList();
        }

        public async Task CreateAsync(ProductDTO dto)
        {
            Category category = await categories.GetAsync(dto.CategoryId);

            Product product = new Product()
            {
                Sku = dto.Sku,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Category = category,
                Tags = dto.Tags
            };

            await products.CreateAsync(product);
        }

        public async Task UpdateAsync(ProductDTO dto)
        {
            Category category = await categories.GetAsync(dto.CategoryId);

            Product product = new Product()
            {
                Sku = dto.Sku,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Category = category,
                Tags = dto.Tags
            };

            await products.CreateAsync(product);
        }

        public async Task DeleteAsync(string sku)
        {
            await products.DeleteAsync(sku);
        }
    }
}
