using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithCountSpecification : BaseSpecification<Product, int>
    {
        public ProductWithCountSpecification(ProductSpecificationParameters productSpecs)
            : base
            (
                  p =>
                  (string.IsNullOrEmpty(productSpecs.Search) || p.Name.ToLower().Contains(productSpecs.Search.ToLower())) &&
                  (!productSpecs.BrandId.HasValue || p.BrandId == productSpecs.BrandId) &&
                  (!productSpecs.TypeId.HasValue || p.TypeId == productSpecs.TypeId)

            )
        {

        }
    }
}
