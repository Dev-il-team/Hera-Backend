using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.ValueObjects;

namespace Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Commands;

public record SubscribeCommand(int ProfileId, EPlanType Plan);
