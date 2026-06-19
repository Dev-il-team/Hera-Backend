using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace DevilTeam.Hera.Platform.DevicesManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDevicesManagementConfiguration(this ModelBuilder builder)
    {
        // Devices Management Context

        builder.Entity<Device>().HasKey(d => d.Id);
        builder.Entity<Device>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Device>().Property(d => d.Name).IsRequired();
        builder.Entity<Device>().Property(d => d.Type).IsRequired();
        builder.Entity<Device>().Property(d => d.Room).IsRequired();
        builder.Entity<Device>().Property(d => d.Status).IsRequired();
        builder.Entity<Device>().Property(d => d.EnergyConsumption).IsRequired();
    }
}