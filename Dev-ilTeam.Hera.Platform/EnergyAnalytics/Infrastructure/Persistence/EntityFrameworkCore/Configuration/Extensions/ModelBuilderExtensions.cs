using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyEnergyAnalyticsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<ConsumptionReport>().HasKey(r => r.Id);
        builder.Entity<ConsumptionReport>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ConsumptionReport>().Property(r => r.DeviceName).IsRequired().HasMaxLength(80);
        builder.Entity<ConsumptionReport>().Property(r => r.Period).IsRequired().HasMaxLength(20);
        builder.Entity<ConsumptionReport>().OwnsOne(r => r.Measurement, m =>
        {
            m.WithOwner().HasForeignKey("Id");
            m.Property(p => p.KilowattHours).HasColumnName("KilowattHours");
            m.Property(p => p.Cost).HasColumnName("Cost");
        });
    }
}