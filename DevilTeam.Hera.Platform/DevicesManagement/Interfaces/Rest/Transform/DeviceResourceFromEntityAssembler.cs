using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;
using DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Resources;

namespace DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Transform;

/// <summary>
///     Transforms a <see cref="Device" /> aggregate into a <see cref="DeviceResource" />.
/// </summary>
public static class DeviceResourceFromEntityAssembler
{
    public static DeviceResource ToResourceFromEntity(Device entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        return new DeviceResource(
            entity.Id,
            entity.DisplayName,
            entity.Type.ToString(),
            entity.Room,
            entity.IsOn,
            entity.Connectivity.ToString(),
            entity.DeviceCodeValue,
            entity.OwnerId,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
