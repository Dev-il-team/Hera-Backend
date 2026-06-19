namespace DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Resources;

/// <summary>
///     Device resource for REST API.
/// </summary>
/// <param name="Id">
///     The unique identifier of the device.
/// </param>
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
public record DeviceResource(
    int Id,
    string Name,
    string Type,
    string Room,
    string Status,
    decimal EnergyConsumption);