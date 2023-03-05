using UsersService.Business.Dtos;

namespace UsersService.Business.Interfaces.Services;
public interface IUserService
{
    Task<IReadOnlyCollection<UserDto>> GetAllAsync();

    Task<UserDto> GetByIdAsync(int id);

    Task<UserDto> AddAsync(UserDto user);

    Task<UserDto> UpdateAsync(UserDto user);

    Task DeleteByIdAsync(int userId);
}
