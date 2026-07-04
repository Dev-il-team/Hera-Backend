using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Subscriptions.Interfaces.Rest.Resources;

namespace Dev_ilTeam.Hera.Platform.Subscriptions.Interfaces.Rest.Transform;

public static class SubscribeCommandFromResourceAssembler
{
    public static SubscribeCommand ToCommandFromResource(SubscribeResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "SubscribeResource cannot be null when converting to command.");
        return new SubscribeCommand(resource.ProfileId, resource.Plan);
    }
}
