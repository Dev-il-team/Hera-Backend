using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;

namespace Dev_ilTeam.Hera.Platform.Subscriptions.Application.CommandServices;

public interface ISubscriptionCommandService
{
    Task<Result<Subscription>> Handle(SubscribeCommand command, CancellationToken cancellationToken);
}
