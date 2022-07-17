using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;
        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
          var products= await _repo.GetProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            return await _repo.GetProductByIdAsync(id);
        }
       
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _repo.GetProductBrandsAsync();
            return Ok(productBrands);
        }
        [HttpGet("tybes")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductTypes()
        {
            var productTypes = await _repo.GetProductTypesAsync();
            return Ok(productTypes);
        }
    }
}
