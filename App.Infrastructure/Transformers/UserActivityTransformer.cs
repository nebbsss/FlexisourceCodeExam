using App.Infrastructure.Dtos;
using App.Models.Entities;

namespace App.Infrastructure.Transformers;

public static class UserActivityTransformer
{
    public static UserActivityDto ToDto(this UserActivityEntity entity)
    {
        return new UserActivityDto()
        {
            AveragePace = entity.AveragePace,
            DateCreated = entity.DateCreated,
            DateUpdated = entity.DateUpdated,
            Distance = entity.Distance,
            Duration = entity.Duration,
            EndDate = entity.EndDate,
            IsActive = entity.IsActive,
            StartedDate = entity.StartedDate,
            UserActivityId = entity.UserActivityId,
            UserId = entity.UserId,
        };
    }
}
