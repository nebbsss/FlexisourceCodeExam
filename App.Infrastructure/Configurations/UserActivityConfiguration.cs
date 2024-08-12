using App.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Configurations;

public class UserActivityConfiguration : IEntityTypeConfiguration<UserActivityEntity>
{
    public void Configure(EntityTypeBuilder<UserActivityEntity> builder)
    {
        builder.ToTable("UserActivities");
        builder.Property(e => e.Id).HasColumnName("UserActivityId");
        builder.HasKey(e => e.UserActivityId);
        builder.Ignore(e => e.Id);

        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.StartedDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x => x.Distance).IsRequired();
        builder.Property(x => x.Duration).IsRequired();
        builder.Property(x => x.AveragePace).IsRequired();
        builder.Property(x => x.DateCreated).IsRequired();
        builder.Property(x => x.DateUpdated).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
    }
}
