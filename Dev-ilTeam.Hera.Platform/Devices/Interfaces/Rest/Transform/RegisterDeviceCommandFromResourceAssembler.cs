using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Devices.Interfaces.Rest.Resources;

namespace Dev_ilTeam.Hera.Platform.Devices.Interfaces.Rest.Transform;

public static class RegisterDeviceCommandFromResourceAssembler
{
    public static RegisterDeviceCommand ToCommandFromResource(RegisterDeviceResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "RegisterDeviceResource cannot be null when converting to command.");
        return new RegisterDeviceCommand(resource.Name, resource.Type, resource.RoomId);
    }
}
