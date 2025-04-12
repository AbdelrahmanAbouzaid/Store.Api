﻿using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductServices(IUnitOfWork unitOfWork,IMapper mapper) : IProductServices
    {
        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync();
            return mapper.Map<IEnumerable<ProductResultDto>>(products);
        }
        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var product =await unitOfWork.GetRepository<Product,int>().GetAsync(id);
            if (product == null) return null;
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
