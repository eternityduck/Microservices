using AutoMapper;
using UsersService.Business.Dtos;
using UsersService.Data.Entities;

namespace UsersService.Business;
public sealed class AutoMapperBusinessProfile : Profile
{
    public AutoMapperBusinessProfile() 
    {
        CreateMap<UserDto, User>()
            .ReverseMap();
    }
}
