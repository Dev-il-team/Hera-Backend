using Dev_ilTeam.Hera.Platform.Profiles.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Profiles.Interfaces.Rest.Resources;

namespace Dev_ilTeam.Hera.Platform.Profiles.Interfaces.Rest.Transform;

public static class CreateProfileCommandFromResourceAssembler
{
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateProfileResource cannot be null when converting to command.");
        return new CreateProfileCommand(
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Street,
            resource.Number,
            resource.City,
            resource.PostalCode,
            resource.Country
        );
    }
}
