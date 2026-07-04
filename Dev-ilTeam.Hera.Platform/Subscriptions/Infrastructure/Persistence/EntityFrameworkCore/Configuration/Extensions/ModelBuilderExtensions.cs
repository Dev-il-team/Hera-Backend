using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Dev_ilTeam.Hera.Platform.Subscriptions.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySubscriptionsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Subscription>().HasKey(s => s.Id);
        builder.Entity<Subscription>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Subscription>().Property(s => s.ProfileId).IsRequired();
        builder.Entity<Subscription>().Property(s => s.Plan).IsRequired();
        builder.Entity<Subscription>().Property(s => s.IsActive).IsRequired();
    }
}
