using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Monitoring.Interfaces.Rest.Resources;

namespace Dev_ilTeam.Hera.Platform.Monitoring.Interfaces.Rest.Transform;

/// <summary>
/// Assembler responsible for transforming a <see cref="RegisterCameraResource" /> into an
/// <see cref="RegisterCameraCommand" />.
/// </summary>
public static class RegisterCameraCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="RegisterCameraResource" /> to a <see cref="RegisterCameraCommand" />.
    /// </summary>
    /// <param name="resource">
    /// The <see cref="RegisterCameraResource" /> containing the camera data. Must not be null.
    /// </param>
    /// <returns>
    /// A new <see cref="RegisterCameraCommand" /> instance.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the input <paramref name="resource" /> is null.</exception>
    public static RegisterCameraCommand ToCommandFromResource(RegisterCameraResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "RegisterCameraResource cannot be null when converting to command.");
        return new RegisterCameraCommand(resource.Name, resource.Location, resource.StreamUrl);
    }
}
