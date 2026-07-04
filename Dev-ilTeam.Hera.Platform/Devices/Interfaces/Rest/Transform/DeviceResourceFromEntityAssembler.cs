using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Devices.Interfaces.Rest.Resources;

namespace Dev_ilTeam.Hera.Platform.Devices.Interfaces.Rest.Transform;

public static class DeviceResourceFromEntityAssembler
{
    public static DeviceResource ToResourceFromEntity(Device entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "Device entity cannot be null when converting to resource.");
        return new DeviceResource(entity.Id, entity.Name, entity.Type.ToString(), entity.Status.ToString(),
            entity.RoomId);
    }
}
