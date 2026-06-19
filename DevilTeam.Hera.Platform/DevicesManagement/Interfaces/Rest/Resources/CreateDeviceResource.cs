namespace DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Resources;

/// <summary>
///     Resource for linking a new device (US11 / US37). <c>Name</c> and <c>Code</c> are required;
///     <c>Type</c> must be one of the DeviceType values (Light, Plug, Thermostat, Camera, Sensor, Other).
/// </summary>
public record CreateDeviceResource(
    string Name,
    string Type,
    string Room,
    string Code,
    int? OwnerId);
