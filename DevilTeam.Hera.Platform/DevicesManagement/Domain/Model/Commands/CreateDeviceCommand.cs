namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Commands;

/// <summary>
///     Command to create a device.
/// </summary>
/// <param name="Name">The device name.</param>
/// <param name="Type">The device type.</param>
/// <param name="Room">The device room.</param>
/// <param name="Status">The device status.</param>
/// <param name="EnergyConsumption">The device energy consumption.</param>
public record CreateDeviceCommand(
    string Name,
    string Type,
    string Room,
    string Status,
    decimal EnergyConsumption);