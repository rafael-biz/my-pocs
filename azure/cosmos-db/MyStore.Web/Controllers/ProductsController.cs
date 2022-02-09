using Microsoft.AspNetCore.Mvc;
using MyStore.Entities;
using MyStore.Repositories.Products;
using MyStore.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Web.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductGet productGet;

        private readonly IProductList productList;

        private readonly IProductCreate productCreate;

        private readonly IProductUpdate productUpdate;

        private readonly IProductDelete productDelete;

        public ProductsController(
            IProductGet productGet,
            IProductList productList,
            IProductCreate productCreate,
            IProductUpdate productUpdate,
            IProductDelete productDelete)
        {
            this.productGet = productGet;
            this.productList = productList;
            this.productCreate = productCreate;
            this.productUpdate = productUpdate;
            this.productDelete = productDelete;
        }

        [HttpGet]
        public async Task<IList<ProductDetailsDTO>> Get()
        {
            IList<ProductDetailsDTO> products = await productList.GetListAsync();

            return products;
        }

        [HttpGet("{sku}")]
        public async Task<ProductDetailsDTO> GetAsync(string sku)
        {
            ProductDetailsDTO product = await productGet.GetAsync(sku);

            return product;
        }

        [HttpPost]
        public async Task Post([FromBody] ProductDTO dto)
        {
            await productCreate.CreateAsync(dto);
        }

        [HttpPut]
        public async Task Put([FromBody] ProductDTO product)
        {
            await productUpdate.UpdateAsync(product);
        }

        [HttpDelete("{sku}")]
        public async Task Delete(string sku)
        {
            await productDelete.DeleteAsync(sku);
        }
    }
}
