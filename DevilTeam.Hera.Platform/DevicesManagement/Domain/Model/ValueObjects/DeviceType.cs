namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.ValueObjects;

/// <summary>
///     The kind of device. A camera is a device of type <see cref="Camera" />, not a separate
///     aggregate; live monitoring (EP04) lives in another context.
/// </summary>
public enum DeviceType
{
    Light,
    Plug,
    Thermostat,
    Camera,
    Sensor,
    Other
}
