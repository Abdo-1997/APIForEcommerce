using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductWithFiltersForCountSpecification:BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParms ProductPrams) : base(x =>
             (string.IsNullOrEmpty(ProductPrams.Search) || x.Name.ToLower().Contains(ProductPrams.Search)) &&
            (!ProductPrams.BrandId.HasValue || x.ProductBrandId == ProductPrams.BrandId) &&
            (!ProductPrams.TypeId.HasValue || x.ProductTypeId == ProductPrams.TypeId)
           )
        {

        }
    }
}
