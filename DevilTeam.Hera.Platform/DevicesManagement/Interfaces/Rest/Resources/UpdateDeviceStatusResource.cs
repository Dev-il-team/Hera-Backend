namespace DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Resources;

/// <summary>
///     Resource for turning a device on or off (US17 / US38).
/// </summary>
public record UpdateDeviceStatusResource(bool IsOn);
