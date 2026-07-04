using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Shared.Domain.Repositories;

namespace Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Repositories;

public interface ISubscriptionRepository : IBaseRepository<Subscription>
{
    Task<Subscription?> FindByProfileIdAsync(int profileId, CancellationToken cancellationToken);
    Task<bool> ExistsByProfileIdAsync(int profileId, CancellationToken cancellationToken);
}
