using UsersService.Business.Dtos;

namespace UsersService.Api.Results;

public sealed record UserResult(
    int Id,
    string FirstName,
    string? LastName,
    string? PhoneNumber,
    string? Email,
    DateOnly? BirthDay,
    IEnumerable<ProductResult> Products)
{
    public static UserResult Create(UserDto userDto)
        => new(
            userDto.Id, 
            userDto.FirstName, 
            userDto.LastName, 
            userDto.PhoneNumber, 
            userDto.Email, 
            userDto.BirthDay,
            userDto.Products.Select(o => ProductResult.Create(o)));
}
