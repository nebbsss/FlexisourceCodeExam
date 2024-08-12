using App.Services.User.Commands;
using FluentValidation;

namespace App.Services.User.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(u => u.Weight)
            .NotNull()
            .GreaterThan(0);

        RuleFor(u => u.Height)
            .NotNull()
            .GreaterThan(0);

        RuleFor(u => u.BirthDate)
            .NotNull();
    }
}