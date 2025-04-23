using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductServices(IUnitOfWork unitOfWork, IMapper mapper) : IProductServices
    {
        public async Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters productSpecs)
        {
            var spec = new ProductWithBrandAndTypeSpecification(productSpecs);

            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);


            var specCount = new ProductWithCountSpecification(productSpecs);

            var count = await unitOfWork.GetRepository<Product, int>().CountAsync(specCount);

            var result = mapper.Map<IEnumerable<ProductResultDto>>(products);

            return new PaginationResponse<ProductResultDto>(productSpecs.PageIndex, productSpecs.PageSize, count, result);
        }
        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecification(id);

            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(spec);
            if (product == null) throw new ProductNotFoundException(id);
            return mapper.Map<ProductResultDto>(product);
        }
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return mapper.Map<IEnumerable<BrandResultDto>>(brands);
        }
        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return mapper.Map<IEnumerable<TypeResultDto>>(types);

        }
    }
}
