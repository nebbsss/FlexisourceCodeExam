using App.Infrastructure.Repositories;
using App.Models.Entities;
using App.Services.UserActivity.Commands;
using App.Services.UserActivity.Validators;
using Moq;

namespace App.Tests.App.Services.Tests.UserActivity;

public class CreateUserActivityValidatorTest
{
    private readonly IRepository<UserEntity> userRepository = Mock.Of<IRepository<UserEntity>>();
    private readonly CreateUserActivityValidator validator;


    public CreateUserActivityValidatorTest()
    {
        validator = new CreateUserActivityValidator(userRepository);
    }

    [Fact]
    public async void NotAllowGuidEmptyUserId()
    {
        var command = new CreateUserActivityCommand()
        {
            UserId = Guid.Empty,
        };

        var result = await validator.ValidateAsync(command);

        Assert.Contains("UserId", result.Errors.Select(c => c.PropertyName));
    }

    [Fact]
    public async void NotAllowStartedDateAndEndDate()
    {
        var command = new CreateUserActivityCommand()
        {
            StartedDate = null,
            EndDate = null,
        };

        var result = await validator.ValidateAsync(command);

        Assert.Contains("StartedDate", result.Errors.Select(c => c.PropertyName));
        Assert.Contains("EndDate", result.Errors.Select(c => c.PropertyName));
    }

    [Fact]
    public async void NotAllowZeroDistance()
    {
        var command = new CreateUserActivityCommand()
        {
            Distance = 0,
        };

        var result = await validator.ValidateAsync(command);

        Assert.Contains("Distance", result.Errors.Select(c => c.PropertyName));
    }


    [Fact]
    public async void ValidationSuccessful()
    {
        var command = new CreateUserActivityCommand()
        {
            UserId = Guid.Parse("6c6d7569-1320-41f9-9e81-5782cfba6296"),
            StartedDate = DateTime.Today,
            EndDate = DateTime.Today.AddHours(1),
            Distance = 1
        };

        Mock.Get(userRepository).Setup(u => u.GetById(command.UserId, It.IsAny<CancellationToken>())).ReturnsAsync(new UserEntity());

        var result = await validator.ValidateAsync(command);

        Assert.True(result.IsValid);
    }
}
