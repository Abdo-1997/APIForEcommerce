using API.Dtos;
using API.Error;
using API.Helpers;
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
  
    public class ProductController : BaseApiController
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
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParms ProductParms)
        {
            var spec = new ProductsWithTypesAndBrandsSpec(ProductParms);

            var countSpec = new ProductWithFiltersForCountSpecification(ProductParms);

            var totalItems =await _productrepo.GetCountAsync(countSpec);
           
            var products = await _productrepo.ListAsync(spec);

            var data = _mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(new Pagination<ProductToReturnDto>
                (ProductParms.PageSize,ProductParms.PageIndx,totalItems,data));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]      //for swwager Documentation 
        [ProducesResponseType(StatusCodes.Status404NotFound)]//for swwager documentation
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpec(id);//call constractor wit id 
          
            var product =  await _productrepo.GetEntityWithSpec(spec);
            //for Clean Documentation to tell swager 
            if (product == null) return NotFound(new ApiResponse(404));

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
