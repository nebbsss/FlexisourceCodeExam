using App.Services.User.Commands;
using App.Services.User.Validators;

namespace App.Tests.App.Services.Tests.User;

public class CreateUserValidatorTest
{
    private readonly CreateUserValidator validator = new CreateUserValidator();

    [Fact]
    public void NotAllowEmptyName()
    {
        var command = new CreateUserCommand()
        {
            Name = null,
        };

        var result = validator.Validate(command);

        Assert.Contains("Name", result.Errors.Select(c => c.PropertyName));
    }

    [Fact]
    public void NotAllowZeroWeight()
    {
        var command = new CreateUserCommand()
        {
            Weight = null,
        };

        var result = validator.Validate(command);

        Assert.Contains("Weight", result.Errors.Select(c => c.PropertyName));
    }

    [Fact]
    public void NotAllowZeroHeight()
    {
        var command = new CreateUserCommand()
        {
            Height = null,
        };

        var result = validator.Validate(command);

        Assert.Contains("Height", result.Errors.Select(c => c.PropertyName));
    }

    [Fact]
    public void NotAllowNullBirthDate()
    {
        var command = new CreateUserCommand()
        {
            BirthDate = null,
        };

        var result = validator.Validate(command);

        Assert.Contains("BirthDate", result.Errors.Select(c => c.PropertyName));
    }

    [Fact]
    public void ValidationSuccessful()
    {
        var command = new CreateUserCommand()
        {
            Name = "Nelson Nebre",
            BirthDate = new DateTime(year: 1994, month: 11, day: 28),
            Height = 170,
            Weight = 86
        };

        var result = validator.Validate(command);

        Assert.True(result.IsValid);
    }
}
