using App.Services.UserActivity.Commands;

namespace App.Tests.App.Services.Tests.UserActivity;

public class CreateUserActivityCommandTest
{

    [Fact]
    public void InitiateCreateUserActivityCommand()
    {
        var command = new CreateUserActivityCommand()
        {
            UserId = Guid.Parse("6c6d7569-1320-41f9-9e81-5782cfba6296"),
            StartedDate = DateTime.Today,
            EndDate = DateTime.Today.AddHours(1),
            Distance = 1
        };

        Assert.Equal(Guid.Parse("6c6d7569-1320-41f9-9e81-5782cfba6296"), command.UserId);
        Assert.Equal(DateTime.Today, command.StartedDate);
        Assert.Equal(DateTime.Today.AddHours(1), command.EndDate);
        Assert.Equal(1, command.Distance);
        Assert.Equal(60, command.Duration);
        Assert.Equal(60, command.AveragePace);
    }

    [Fact]
    public void DurationAndAveragePaceMustBeNull()
    {
        var command = new CreateUserActivityCommand()
        {
            StartedDate = null,
            EndDate = null
        };

        Assert.Null(command.Duration);
        Assert.Null(command.AveragePace);
    }
}
