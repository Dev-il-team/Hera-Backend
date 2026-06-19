namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Commands;

/// <summary>
///     Rename a device (US13).
/// </summary>
public record UpdateDeviceNameCommand(int DeviceId, string Name);
