using Dev_ilTeam.Hera.Platform.Profiles.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Profiles.Interfaces.Rest.Resources;

namespace Dev_ilTeam.Hera.Platform.Profiles.Interfaces.Rest.Transform;

public static class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "Profile entity cannot be null when converting to resource.");
        return new ProfileResource(entity.Id, entity.FullName, entity.EmailAddress, entity.StreetAddress);
    }
}
