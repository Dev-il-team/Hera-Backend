using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace DevilTeam.Hera.Platform.DevicesManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDevicesManagementConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Device>().HasKey(d => d.Id);
        builder.Entity<Device>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Entity<Device>().OwnsOne(d => d.Name,
            n =>
            {
                n.WithOwner().HasForeignKey("Id");
                n.Property(p => p.Value).HasColumnName("Name").IsRequired().HasMaxLength(50);
            });

        builder.Entity<Device>().OwnsOne(d => d.Code,
            c =>
            {
                c.WithOwner().HasForeignKey("Id");
                c.Property(p => p.Value).HasColumnName("Code").IsRequired();
            });

        builder.Entity<Device>().Property(d => d.Type).HasConversion<string>().IsRequired();
        builder.Entity<Device>().Property(d => d.Connectivity).HasConversion<string>().IsRequired();
        builder.Entity<Device>().Property(d => d.Room).IsRequired();
        builder.Entity<Device>().Property(d => d.IsOn).IsRequired();
        builder.Entity<Device>().Property(d => d.OwnerId);
    }
}
