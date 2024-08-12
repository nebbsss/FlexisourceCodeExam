using App.Services.User.Commands;

namespace App.Tests.App.Services.Tests.User;

public class CreateUserCommandTest
{

    [Fact]
    public void InitiateCreateUserCommand()
    {
        var command = new CreateUserCommand()
        {
            Name = "Nelson Nebre",
            BirthDate = new DateTime(year: 1994, month: 11, day: 28),
            Height = 170,
            Weight = 86
        };

        Assert.Equal("Nelson Nebre", command.Name);
        Assert.Equal(new DateTime(year: 1994, month: 11, day: 28), command.BirthDate);
        Assert.Equal(170, command.Height);
        Assert.Equal(86, command.Weight);
        Assert.Equal(29.757785467128031, command.Bmi);
        Assert.Equal(30, command.Age);
    }

    [Fact]
    public void AgeMustBeNull()
    {
        var command = new CreateUserCommand()
        {
            BirthDate = null
        };

        Assert.Null(command.Age);
    }

    [Fact]
    public void BmiMustBeNull()
    {
        var command = new CreateUserCommand()
        {
            Height = null,
            Weight = null
        };

        Assert.Null(command.Bmi);
    }
}
