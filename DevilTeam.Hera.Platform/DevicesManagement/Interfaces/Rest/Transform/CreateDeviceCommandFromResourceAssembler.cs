using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Commands;
using DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Resources;

namespace DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="CreateDeviceResource"/> into a
///     <see cref="CreateDeviceCommand"/>.
/// </summary>
public static class CreateDeviceCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="CreateDeviceResource"/> to a <see cref="CreateDeviceCommand"/>.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateDeviceResource"/> containing the data for creating a device.
    /// </param>
    /// <returns>
    ///     A new <see cref="CreateDeviceCommand"/> instance.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the input resource is null.
    /// </exception>
    public static CreateDeviceCommand ToCommandFromResource(CreateDeviceResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateDeviceResource cannot be null when converting to command.");

        return new CreateDeviceCommand(
            resource.Name,
            resource.Type,
            resource.Room,
            resource.Status,
            resource.EnergyConsumption
        );
    }
}