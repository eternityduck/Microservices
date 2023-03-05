using FluentValidation;
using UsersService.Api.Models;

namespace UsersService.Api.Validators;

public sealed class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
{
    public UpdateUserModelValidator()
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
