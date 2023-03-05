using FluentValidation;
using UsersService.Api.Models;

namespace UsersService.Api.Validators;

public sealed class CreateUserModelValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserModelValidator()
    {
        RuleFor(u => u.FirstName)
            .NotEmpty()
            .WithMessage("FirstName must not be empty");

        RuleFor(u => u.Email)
            .EmailAddress()
            .WithMessage("The email address is not valid");

        RuleFor(u => u.PhoneNumber)
            .Must(p => p is null || p.All(c => char.IsNumber(c)))
            .WithMessage("Phone number must contain only digits");
    }
}
