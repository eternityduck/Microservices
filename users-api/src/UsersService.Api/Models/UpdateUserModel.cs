namespace UsersService.Api.Models;

public sealed record UpdateUserModel(
    string FirstName,
    string? LastName,
    string? PhoneNumber,
    string? Email,
    DateOnly? BirthDay);
