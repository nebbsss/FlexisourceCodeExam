namespace App.Infrastructure.Dtos;

public class UserActivityDto : BaseDto
{
    public Guid UserActivityId { get; set; }
    public Guid UserId { get; set; }
    public DateTime? StartedDate { get; set; }
    public DateTime? EndDate { get; set; }
    public double? Distance { get; set; }
    public double? Duration { get; set; }
    public double? AveragePace { get; set; }
}
