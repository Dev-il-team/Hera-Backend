using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Dev_ilTeam.Hera.Platform.Monitoring.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyMonitoringConfiguration(this ModelBuilder builder)
    {
        // Monitoring Context
        builder.Entity<Camera>().HasKey(c => c.Id);
        builder.Entity<Camera>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Camera>().Property(c => c.Name).IsRequired().HasMaxLength(80);
        builder.Entity<Camera>().Property(c => c.Location).IsRequired().HasMaxLength(80);
        builder.Entity<Camera>().Property(c => c.StreamUrl).IsRequired().HasMaxLength(255);
        builder.Entity<Camera>().Property(c => c.Status).IsRequired();
    }
}
