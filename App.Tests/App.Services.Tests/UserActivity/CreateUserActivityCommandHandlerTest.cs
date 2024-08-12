using App.Infrastructure.Repositories;
using App.Models.Entities;
using App.Services.UserActivity.Commands;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;

namespace App.Tests.App.Services.Tests.UserActivity;

public class CreateUserActivityCommandHandlerTest
{
    private ILogger<CreateUserActivityCommandHandler> logger = Mock.Of<ILogger<CreateUserActivityCommandHandler>>();
    private readonly IRepository<UserActivityEntity> userActivityRepository = Mock.Of<IRepository<UserActivityEntity>>();
    private readonly IValidator<CreateUserActivityCommand> validator = Mock.Of<IValidator<CreateUserActivityCommand>>();
    private readonly CreateUserActivityCommandHandler systemUnderTest;

    public CreateUserActivityCommandHandlerTest()
    {
        systemUnderTest = new(validator, userActivityRepository, logger);
    }

    [Fact]
    public async void ShouldCallValidateAsyncOnce()
    {
        var command = new CreateUserActivityCommand()
        {
            UserId = Guid.Parse("6c6d7569-1320-41f9-9e81-5782cfba6296"),
            StartedDate = DateTime.Today,
            EndDate = DateTime.Today.AddHours(1),
            Distance = 1
        };

        var cancellatioToken = It.IsAny<CancellationToken>();

        Mock.Get(validator).Setup(v => v.ValidateAsync(command, cancellatioToken)).ReturnsAsync(new ValidationResult());

        await systemUnderTest.Handle(command, cancellatioToken);

        Mock.Get(validator).Verify(v => v.ValidateAsync(command, cancellatioToken), Times.Once);
    }

    [Fact]
    public async void ShouldCallUserRepositoryOnce()
    {
        var command = new CreateUserActivityCommand()
        {
            UserId = Guid.Parse("6c6d7569-1320-41f9-9e81-5782cfba6296"),
            StartedDate = DateTime.Today,
            EndDate = DateTime.Today.AddHours(1),
            Distance = 1
        };
        var cancellatioToken = It.IsAny<CancellationToken>();

        Mock.Get(validator).Setup(v => v.ValidateAsync(command, cancellatioToken)).ReturnsAsync(new ValidationResult());

        await systemUnderTest.Handle(command, cancellatioToken);

        Mock.Get(userActivityRepository).Verify(r => r.Create(It.IsAny<UserActivityEntity>(), cancellatioToken), Times.Once);
    }

    [Fact]
    public async void ShouldReturnSuccessCreation()
    {
        var command = new CreateUserActivityCommand()
        {
            UserId = Guid.Parse("6c6d7569-1320-41f9-9e81-5782cfba6296"),
            StartedDate = DateTime.Today,
            EndDate = DateTime.Today.AddHours(1),
            Distance = 1
        };
        var cancellatioToken = It.IsAny<CancellationToken>();

        Mock.Get(validator).Setup(v => v.ValidateAsync(command, cancellatioToken)).ReturnsAsync(new ValidationResult());

        var result = await systemUnderTest.Handle(command, cancellatioToken);

        Assert.True(result.Success);
        Assert.Null(result.Errors);
    }

    [Fact]
    public async void ShouldReturnFailedCreation()
    {
        var command = new CreateUserActivityCommand();
        var cancellatioToken = It.IsAny<CancellationToken>();
        var validationResult = new ValidationResult()
        {
            Errors = new List<ValidationFailure>()
            {

                new ValidationFailure("UserId", "UserId is required")
            }
        };

        Mock.Get(validator).Setup(v => v.ValidateAsync(command, cancellatioToken)).ReturnsAsync(validationResult);

        var result = await systemUnderTest.Handle(command, cancellatioToken);

        Assert.False(result.Success);
        Assert.NotNull(result.Errors);
        Assert.NotEmpty(result.Errors);
    }
}
