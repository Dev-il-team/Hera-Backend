namespace DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Resources;

/// <summary>
///     Resource for creating a new device.
/// </summary>
/// <param name="Name">
///     The name of the device.
/// </param>
/// <param name="Type">
///     The type of the device.
/// </param>
/// <param name="Room">
///     The room where the device is located.
/// </param>
/// <param name="Status">
///     The current status of the device.
/// </param>
/// <param name="EnergyConsumption">
///     The energy consumption of the device.
/// </param>
public record CreateDeviceResource(
    string Name,
    string Type,
    string Room,
    string Status,
    decimal EnergyConsumption);