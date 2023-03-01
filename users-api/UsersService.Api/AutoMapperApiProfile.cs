using AutoMapper;
using UsersService.Api.Models;
using UsersService.Business.Dtos;

namespace UsersService.Api;

public sealed class AutoMapperApiProfile : Profile
{
    public AutoMapperApiProfile() 
    {
        CreateMap<CreateUserModel, UserDto>();
        CreateMap<UpdateUserModel, UserDto>();
    }
}
