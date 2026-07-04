using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dev_ilTeam.Hera.Platform.Devices.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDevicesConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Room>().HasKey(r => r.Id);
        builder.Entity<Room>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Room>().Property(r => r.Name).IsRequired().HasMaxLength(60);

        builder.Entity<Device>().HasKey(d => d.Id);
        builder.Entity<Device>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Device>().Property(d => d.Name).IsRequired().HasMaxLength(80);
        builder.Entity<Device>().Property(d => d.Type).IsRequired();
        builder.Entity<Device>().Property(d => d.Status).IsRequired();
        builder.Entity<Device>().HasOne(d => d.Room)
            .WithMany()
            .HasForeignKey(d => d.RoomId);
    }
}
