using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IProductServices
    {
        //Get All product
        Task<IEnumerable<ProductResultDto>> GetAllProductsAsync();

        //Get ProductBy Id
        Task<ProductResultDto?> GetProductByIdAsync(int id);

        //Get All Brand
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();

        //Get All Type
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();

    }
}
