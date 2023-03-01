namespace UsersService.Api.Models;

public sealed record CreateUserModel(
    string FirstName,
    string? LastName,
    string? PhoneNumber,
    string? Email);
