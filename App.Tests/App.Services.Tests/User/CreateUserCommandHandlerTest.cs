using App.Infrastructure.Repositories;
using App.Models.Entities;
using App.Services.User.Commands;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;

namespace App.Tests.App.Services.Tests.User;

public class CreateUserCommandHandlerTest
{
    private ILogger<CreateUserCommandHandler> logger = Mock.Of<ILogger<CreateUserCommandHandler>>();
    private readonly IRepository<UserEntity> userRepository = Mock.Of<IRepository<UserEntity>>();
    private readonly IValidator<CreateUserCommand> validator = Mock.Of<IValidator<CreateUserCommand>>();
    private readonly CreateUserCommandHandler systemUnderTest;

    public CreateUserCommandHandlerTest()
    {
        systemUnderTest = new(validator, userRepository, logger);
    }

    [Fact]
    public async void ShouldCallValidateAsyncOnce()
    {
        var command = new CreateUserCommand()
        {
            Name = "Nelson Nebre",
            BirthDate = new DateTime(year: 1994, month: 11, day: 28),
            Height = 170,
            Weight = 86
        };
        var cancellatioToken = It.IsAny<CancellationToken>();

        Mock.Get(validator).Setup(v => v.ValidateAsync(command, cancellatioToken)).ReturnsAsync(new ValidationResult());

        await systemUnderTest.Handle(command, cancellatioToken);

        Mock.Get(validator).Verify(v => v.ValidateAsync(command, cancellatioToken), Times.Once);
    }

    [Fact]
    public async void ShouldCallUserRepositoryOnce()
    {
        var command = new CreateUserCommand()
        {
            Name = "Nelson Nebre",
            BirthDate = new DateTime(year: 1994, month: 11, day: 28),
            Height = 170,
            Weight = 86
        };
        var cancellatioToken = It.IsAny<CancellationToken>();

        Mock.Get(validator).Setup(v => v.ValidateAsync(It.IsAny<CreateUserCommand>(), cancellatioToken)).ReturnsAsync(new ValidationResult());

        await systemUnderTest.Handle(command, cancellatioToken);

        Mock.Get(userRepository).Verify(r => r.Create(It.IsAny<UserEntity>(), cancellatioToken), Times.Once);
    }

    [Fact]
    public async void ShouldReturnSuccessCreation()
    {
        var command = new CreateUserCommand()
        {
            Name = "Nelson Nebre",
            BirthDate = new DateTime(year: 1994, month: 11, day: 28),
            Height = 170,
            Weight = 86
        };
        var cancellatioToken = It.IsAny<CancellationToken>();

        Mock.Get(validator).Setup(v => v.ValidateAsync(It.IsAny<CreateUserCommand>(), cancellatioToken)).ReturnsAsync(new ValidationResult());

        var result = await systemUnderTest.Handle(command, cancellatioToken);

        Assert.True(result.Success);
        Assert.Null(result.Errors);
    }

    [Fact]
    public async void ShouldReturnFailedCreation()
    {
        var command = new CreateUserCommand();
        var cancellatioToken = It.IsAny<CancellationToken>();
        var validationResult = new ValidationResult()
        {
            Errors = new List<ValidationFailure>()
            {

                new ValidationFailure("Name", "Name is required")
            }
        };

        Mock.Get(validator).Setup(v => v.ValidateAsync(It.IsAny<CreateUserCommand>(), cancellatioToken)).ReturnsAsync(validationResult);

        var result = await systemUnderTest.Handle(command, cancellatioToken);

        Assert.False(result.Success);
        Assert.NotNull(result.Errors);
        Assert.NotEmpty(result.Errors);
    }
}
