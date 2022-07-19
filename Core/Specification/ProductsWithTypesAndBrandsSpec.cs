using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductsWithTypesAndBrandsSpec:BaseSpecification<Entities.Product>
    {
        //this constractor to get single Entity including type and brand 
        public ProductsWithTypesAndBrandsSpec(int id ):base(p=>p.Id==id)
        {
            AddToIncludes(p => p.ProductType);
            AddToIncludes(p => p.ProductBrand);
        }
        //This Constractor to get  IQurable of Products 
        public ProductsWithTypesAndBrandsSpec()
        {
            AddToIncludes(p => p.ProductType);
            AddToIncludes(p => p.ProductBrand);
        }
    }
}
