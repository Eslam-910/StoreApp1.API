using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstraction;
using Services.Mapping_Profiles;
using Services.Specifications;
using Shared;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork,IMapper mapper) : IProductService
    {


        //public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(int? brandid, int? typeid, string? sort, int pageindex = 1,int pagesize=5)
        public async Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductspecificationsParameters specparams)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(specparams);
            // Get All Products Throught Product Repository
            var products=await unitOfWork.GetRepository<Product,int>().GetAllAsync(spec);

            var speccount = new ProductWithCountSpecification(specparams);

            var count=await unitOfWork.GetRepository<Product,int>().CountAsync(speccount);

            //Mapping IEnumrable<Product>To IEnumrable<ProductResultDto>:AutoMapper
            var result= mapper.Map<IEnumerable<ProductResultDto>>(products);
            return new PaginationResponse<ProductResultDto>(specparams.PageIndex,specparams.PageSize, count, result);

        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec=new ProductWithBrandAndTypeSpecifications(id);
            var Product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);

            if (Product == null) throw new ProductNotFoundException(id);
            var result = mapper.Map<ProductResultDto>(Product);
        
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
