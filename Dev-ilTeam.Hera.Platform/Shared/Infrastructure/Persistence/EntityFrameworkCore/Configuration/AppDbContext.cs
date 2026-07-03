using Dev_ilTeam.Hera.Platform.Automation.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Dev_ilTeam.Hera.Platform.Devices.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Dev_ilTeam.Hera.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Dev_ilTeam.Hera.Platform.Monitoring.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Dev_ilTeam.Hera.Platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Dev_ilTeam.Hera.Platform.Subscriptions.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyIamConfiguration();

        builder.ApplyProfilesConfiguration();

        builder.ApplyDevicesConfiguration();

        builder.ApplyAutomationConfiguration();

        builder.ApplyMonitoringConfiguration();

        builder.ApplyEnergyAnalyticsConfiguration();

        builder.ApplySubscriptionsConfiguration();

        builder.UseSnakeCaseNamingConvention();
    }
}
