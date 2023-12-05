using AutoMapper;
using Orders.Models;
using Orders.Models.Dto;

namespace Orders
{
    public class MappingHelper : Profile
    {
        public MappingHelper()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
