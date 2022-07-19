using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productrepo;
        private readonly IGenericRepository<ProductBrand> _brandrepo;
        private readonly IGenericRepository<ProductType> _typerepo;
        private readonly IMapper _mapper;
        public ProductController( IGenericRepository<Product> productrepo,
            IGenericRepository<ProductBrand> brandrepo,IGenericRepository<ProductType> typerepo ,
           IMapper mapper )
        {
            _productrepo = productrepo;
            _brandrepo = brandrepo;
            _typerepo = typerepo;
            _mapper = mapper;
        }

        [HttpGet("Products")]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpec();//call parameter less constractor 
            var products = await _productrepo.ListAsync(spec);
            return Ok(_mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpec(id);//call constractor wit id 
            var product =  await _productrepo.GetEntityWithSpec(spec);
            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }
       
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _brandrepo.GetAllAsync();
            return Ok(productBrands);
        }
        [HttpGet("tybes")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var productTypes = await _typerepo.GetAllAsync();
            return Ok(productTypes);
        }
    }
}
