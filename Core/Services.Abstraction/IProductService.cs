using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Services.Abstraction
{
    public interface IProductService
    {
        //GetALL Product
        Task< IEnumerable<ProductResultDto>> GetAllProductsAsync();
        //Get Product By Id
        Task<ProductResultDto?> GetProductByIdAsync(int Id);
        //Get Brands
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        //Get Types
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
    }
}
