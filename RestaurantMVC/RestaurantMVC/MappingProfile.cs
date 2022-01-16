using AutoMapper;
using RestaurantMVC.Entities;
using RestaurantMVC.Models;
using System.Collections.Generic;

namespace RestaurantMVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}
