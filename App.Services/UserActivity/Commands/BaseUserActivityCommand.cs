using MediatR;

namespace App.Services.UserActivity.Commands;

public class BaseUserActivityCommand : IRequest<BaseResponse>
{
    public Guid UserActivityId { get; set; }
    public Guid UserId { get; set; }
    public DateTime? StartedDate { get; set; }
    public DateTime? EndDate { get; set; }
    public double? Distance { get; set; }
    public double? Duration 
    {
        get
        {
            if(!StartedDate.HasValue || !EndDate.HasValue) return null;
            if(StartedDate.Value > EndDate.Value) return null;

            return EndDate.Value.Subtract(StartedDate.Value).TotalMinutes;
        }
    }
    public double? AveragePace
    {
        get
        {
            if (!Distance.HasValue || !Duration.HasValue) return null;

            return Duration.Value / Distance.Value;
        }
    }
}
