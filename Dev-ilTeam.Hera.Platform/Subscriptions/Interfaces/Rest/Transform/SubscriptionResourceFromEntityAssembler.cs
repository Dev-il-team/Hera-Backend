using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Subscriptions.Interfaces.Rest.Resources;

namespace Dev_ilTeam.Hera.Platform.Subscriptions.Interfaces.Rest.Transform;

public static class SubscriptionResourceFromEntityAssembler
{
    public static SubscriptionResource ToResourceFromEntity(Subscription entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "Subscription entity cannot be null when converting to resource.");
        return new SubscriptionResource(entity.Id, entity.ProfileId, entity.Plan.ToString(), entity.IsActive);
    }
}
