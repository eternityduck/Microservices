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
    }
}
