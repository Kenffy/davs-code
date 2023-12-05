using Authenticate.Models.Dto;
using Authenticate.Models;
using AutoMapper;

namespace Authenticate
{
    public class MappingHelper : Profile
    {
        public MappingHelper()
        {
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
