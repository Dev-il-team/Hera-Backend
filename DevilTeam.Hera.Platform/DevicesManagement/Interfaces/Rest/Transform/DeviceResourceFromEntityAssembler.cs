using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;
using DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Resources;

namespace DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="Device"/> aggregate into a <see cref="DeviceResource"/>.
/// </summary>
public static class DeviceResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="Device"/> aggregate to its <see cref="DeviceResource"/> representation.
    /// </summary>
    /// <param name="entity">
    ///     The <see cref="Device"/> aggregate to convert.
    /// </param>
    /// <returns>
    ///     A <see cref="DeviceResource"/> object representing the provided device.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the input entity is null.
    /// </exception>
    public static DeviceResource ToResourceFromEntity(Device entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "Device entity cannot be null when converting to resource.");

        return new DeviceResource(
            entity.Id,
            entity.Name,
            entity.Type,
            entity.Room,
            entity.Status,
            entity.EnergyConsumption
        );
    }
}