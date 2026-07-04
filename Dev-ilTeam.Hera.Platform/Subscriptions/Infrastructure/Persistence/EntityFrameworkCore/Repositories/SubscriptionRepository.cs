using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dev_ilTeam.Hera.Platform.Subscriptions.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SubscriptionRepository(AppDbContext context)
    : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    public async Task<Subscription?> FindByProfileIdAsync(int profileId, CancellationToken cancellationToken)
    {
        return await Context.Set<Subscription>()
            .FirstOrDefaultAsync(s => s.ProfileId == profileId, cancellationToken);
    }

    public async Task<bool> ExistsByProfileIdAsync(int profileId, CancellationToken cancellationToken)
    {
        return await Context.Set<Subscription>().AnyAsync(s => s.ProfileId == profileId, cancellationToken);
    }
}
