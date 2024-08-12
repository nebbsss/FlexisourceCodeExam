using App.Infrastructure.Repositories;
using App.Models.Entities;
using App.Services.UserActivity.Commands;
using FluentValidation;

namespace App.Services.UserActivity.Validators;

public class CreateUserActivityValidator : AbstractValidator<CreateUserActivityCommand>
{
    private readonly IRepository<UserEntity> _userRepository;
    public CreateUserActivityValidator(IRepository<UserEntity> userRepository)
    {
        ArgumentNullException.ThrowIfNull(nameof(userRepository));
        _userRepository = userRepository;

        RuleFor(u => u.UserId)
            .NotNull()
            .NotEqual(Guid.Empty);

        RuleFor(u => u.StartedDate)
            .NotNull();

        RuleFor(u => u.EndDate)
            .NotNull();

        RuleFor(u => u.Distance)
            .GreaterThan(0);

        RuleFor(u => u)
            .MustAsync(CheckUser)
            .WithMessage("Unable to find user!")
            .WithName(u => nameof(u.UserId));
    }

    private async Task<bool> CheckUser(CreateUserActivityCommand command, CancellationToken cancellationToken)
    {
        if (command.UserId == Guid.Empty) return true;
        var ex = (await _userRepository.GetById(command.UserId, cancellationToken)) is not null;
        return ex;
    }
}