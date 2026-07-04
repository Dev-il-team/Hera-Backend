using Dev_ilTeam.Hera.Platform.Subscriptions.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Queries;
using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Repositories;

namespace Dev_ilTeam.Hera.Platform.Subscriptions.Application.Internal.QueryServices;

public class SubscriptionQueryService(ISubscriptionRepository subscriptionRepository) : ISubscriptionQueryService
{
    public async Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsQuery query,
        CancellationToken cancellationToken)
    {
        return await subscriptionRepository.ListAsync(cancellationToken);
    }

    public async Task<Subscription?> Handle(GetSubscriptionByIdQuery query, CancellationToken cancellationToken)
    {
        return await subscriptionRepository.FindByIdAsync(query.SubscriptionId, cancellationToken);
    }

    public async Task<Subscription?> Handle(GetSubscriptionByProfileIdQuery query, CancellationToken cancellationToken)
    {
        return await subscriptionRepository.FindByProfileIdAsync(query.ProfileId, cancellationToken);
    }
}
