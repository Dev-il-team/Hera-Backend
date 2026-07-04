using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Entities;
using Dev_ilTeam.Hera.Platform.Devices.Interfaces.Rest.Resources;

namespace Dev_ilTeam.Hera.Platform.Devices.Interfaces.Rest.Transform;

public static class RoomResourceFromEntityAssembler
{
    public static RoomResource ToResourceFromEntity(Room entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Room entity cannot be null when converting to resource.");
        return new RoomResource(entity.Id, entity.Name);
    }
}
