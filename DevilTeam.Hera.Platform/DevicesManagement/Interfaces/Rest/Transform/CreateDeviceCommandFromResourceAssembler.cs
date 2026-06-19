using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Commands;
using DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Resources;

namespace DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Transform;

/// <summary>
///     Transforms a <see cref="CreateDeviceResource" /> into a <see cref="CreateDeviceCommand" />.
/// </summary>
public static class CreateDeviceCommandFromResourceAssembler
{
    public static CreateDeviceCommand ToCommandFromResource(CreateDeviceResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);
        return new CreateDeviceCommand(
            resource.Name,
            resource.Type,
            resource.Room,
            resource.Code,
            resource.OwnerId);
    }
}
