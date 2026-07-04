using Dev_ilTeam.Hera.Platform.Shared.Domain.Model;

namespace Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Errors;

public static class SubscriptionsErrors
{
    public static readonly Error SubscriptionNotFound =
        new("Subscriptions.SubscriptionNotFound", "The specified subscription was not found.");

    public static readonly Error ProfileAlreadySubscribed =
        new("Subscriptions.ProfileAlreadySubscribed", "The specified profile already has an active subscription.");
}
