using Domain.Models;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product, int>
    {
        public ProductWithBrandAndTypeSpecification() : base(null)
        {
            ApplyInclude();
        }
        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            ApplyInclude();
        }
        public ProductWithBrandAndTypeSpecification(ProductSpecificationParameters productSpecs)
            : base
            (
                  p =>
                  (string.IsNullOrEmpty(productSpecs.Search) || p.Name.ToLower().Contains(productSpecs.Search.ToLower()))&&
                  (!productSpecs.BrandId.HasValue || p.BrandId == productSpecs.BrandId) &&
                  (!productSpecs.TypeId.HasValue || p.TypeId == productSpecs.TypeId)

            )
        {
            ApplyInclude();
            ApplySort(productSpecs.Sort);
            ApplyPagination(productSpecs.PageIndex, productSpecs.PageSize);

        }
        private void ApplyInclude()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        private void ApplySort(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "namedesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "price":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }
        }
    }
}

