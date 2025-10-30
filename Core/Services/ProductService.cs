using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstraction;
using Services.Mapping_Profiles;
using Shared;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork,IMapper mapper) : IProductService
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
            // Get All Products Throught Product Repository
            var products=await unitOfWork.GetRepository<Product,int>().GetAllAsync();
            //Mapping IEnumrable<Product>To IEnumrable<ProductResultDto>:AutoMapper
            var result= mapper.Map<IEnumerable<ProductResultDto>>(products);
            return result;

        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int Id)
        {
           var product= await unitOfWork.GetRepository<Product,int>().GetAsync(Id);

            if(product == null) return null;
            var result= mapper.Map<ProductResultDto>(product);
            return result;

        }

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
           var productbrand= await unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            var result= mapper.Map<IEnumerable<BrandResultDto>>(productbrand);
            return result;
        }        

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var producttype=await unitOfWork.GetRepository<ProductType,int>().GetAllAsync();

            var result= mapper.Map<IEnumerable<TypeResultDto>>(producttype);

            return result;
        }



        
    }
}
