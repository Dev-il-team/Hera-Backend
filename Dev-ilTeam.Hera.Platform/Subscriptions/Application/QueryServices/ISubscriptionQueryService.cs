using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Queries;

namespace Dev_ilTeam.Hera.Platform.Subscriptions.Application.QueryServices;

public interface ISubscriptionQueryService
{
    Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsQuery query, CancellationToken cancellationToken);
    Task<Subscription?> Handle(GetSubscriptionByIdQuery query, CancellationToken cancellationToken);
    Task<Subscription?> Handle(GetSubscriptionByProfileIdQuery query, CancellationToken cancellationToken);
}
