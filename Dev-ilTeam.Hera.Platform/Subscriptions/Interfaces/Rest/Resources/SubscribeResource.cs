using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.ValueObjects;

namespace Dev_ilTeam.Hera.Platform.Subscriptions.Interfaces.Rest.Resources;

public record SubscribeResource(int ProfileId, EPlanType Plan);
