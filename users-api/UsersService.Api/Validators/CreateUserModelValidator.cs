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
    }
}
