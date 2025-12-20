using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Shared;

namespace Services.Mapping_Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResultDto>()
                .ForMember(m => m.BrandName, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(m => m.TypeName, o => o.MapFrom(s => s.ProductType.Name))
                //.ForMember(m=>m.PictureUrl,o=>o.MapFrom(s=>s.PictureUrl))
                .ForMember(m=>m.PictureUrl,o=>o.MapFrom<PictureUrlResolver>())
                ;
            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<ProductType, TypeResultDto>();
        }
    }
}
