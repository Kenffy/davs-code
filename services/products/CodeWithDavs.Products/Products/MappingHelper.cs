using AutoMapper;
using Products.Models;
using Products.Models.Dto;

namespace Products
{
    public class MappingHelper : Profile
    {
        public MappingHelper()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}