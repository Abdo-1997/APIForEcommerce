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
        public ProductsWithTypesAndBrandsSpec(ProductSpecParms ProductPrams) : base(x => 
            (string.IsNullOrEmpty(ProductPrams.Search)|| x.Name.ToLower().Contains(ProductPrams.Search))&&
            (!ProductPrams.BrandId.HasValue || x.ProductBrandId == ProductPrams.BrandId) &&
            (!ProductPrams.TypeId.HasValue || x.ProductTypeId == ProductPrams.TypeId) 
        
           )
        {
            AddToIncludes(p => p.ProductType);
            AddToIncludes(p => p.ProductBrand);
            AddOrderBy(p => p.Name);
            ApplyPaging(ProductPrams.PageSize*(ProductPrams.PageIndx -1),ProductPrams.PageSize);

            if (!string.IsNullOrEmpty(ProductPrams.Sort))
            {
                switch (ProductPrams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p=>p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDes(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

      
    }
}
