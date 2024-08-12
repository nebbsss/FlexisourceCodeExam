namespace App.Models.Entities;

public class UserActivityEntity : BaseEntity, IEntity
{
    public object? Id { get => this.UserActivityId; set => this.UserActivityId = value != null ? (Guid)value : Guid.Empty; }
    public Guid UserActivityId { get; set; }
    public Guid UserId { get; set; }
    public DateTime? StartedDate { get; set; }
    public DateTime? EndDate { get; set; }
    public double? Distance { get; set; }
    public double? Duration { get; set; }
    public double? AveragePace { get; set; }
    public UserEntity? User { get; set; }
}
