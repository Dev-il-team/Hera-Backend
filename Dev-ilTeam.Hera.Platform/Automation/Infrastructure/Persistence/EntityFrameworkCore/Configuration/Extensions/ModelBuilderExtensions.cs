using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Dev_ilTeam.Hera.Platform.Automation.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAutomationConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Routine>().HasKey(r => r.Id);
        builder.Entity<Routine>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Routine>().Property(r => r.Name).IsRequired().HasMaxLength(80);
        builder.Entity<Routine>().Property(r => r.ScheduledTime).IsRequired().HasMaxLength(40);
        builder.Entity<Routine>().Property(r => r.TriggerType).IsRequired();
        builder.Entity<Routine>().Property(r => r.Status).IsRequired();
    }
}
