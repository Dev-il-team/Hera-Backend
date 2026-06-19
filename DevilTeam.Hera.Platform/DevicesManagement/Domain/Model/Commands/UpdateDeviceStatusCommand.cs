namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Commands;

/// <summary>
///     Turn a device on or off (US17 / US38).
/// </summary>
public record UpdateDeviceStatusCommand(int DeviceId, bool IsOn);
