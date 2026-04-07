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
    public class BasketProfile:Profile
    {
        public BasketProfile() 
        {
            CreateMap<CustomerBasket,BasketDto>().ReverseMap();
            CreateMap<BasketItem,BasketItemDto>().ReverseMap();
            //CreateMap<BasketDto, CustomerBasket>()
            // .ForMember(dest => dest.Items,
            //   opt => opt.MapFrom(src => src.ItemsDto));

            //CreateMap<CustomerBasket, BasketDto>()
            //    .ForMember(dest => dest.ItemsDto,
            //               opt => opt.MapFrom(src => src.Items));
        }
    }
}
