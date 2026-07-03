using Dev_ilTeam.Hera.Platform.Iam.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Iam.Interfaces.Rest.Resources;

namespace Dev_ilTeam.Hera.Platform.Iam.Interfaces.Rest.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "SignInResource cannot be null when converting to command.");
        return new SignInCommand(resource.Username, resource.Password);
    }
}
