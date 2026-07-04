namespace Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model;

public enum SubscriptionsError
{
    None,
    SubscriptionNotFound,
    ProfileAlreadySubscribed,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
