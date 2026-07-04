using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Devices.Interfaces.Rest.Resources;

namespace Dev_ilTeam.Hera.Platform.Devices.Interfaces.Rest.Transform;

public static class CreateRoomCommandFromResourceAssembler
{
    public static CreateRoomCommand ToCommandFromResource(CreateRoomResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateRoomResource cannot be null when converting to command.");
        return new CreateRoomCommand(resource.Name);
    }
}
