namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Commands;

/// <summary>
///     Unlink (delete) a device (US14).
/// </summary>
public record DeleteDeviceCommand(int DeviceId);
