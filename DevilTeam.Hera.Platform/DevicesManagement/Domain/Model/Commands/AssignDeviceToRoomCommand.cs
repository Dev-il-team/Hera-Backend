namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Commands;

/// <summary>
///     Assign a device to a room/zone (US15).
/// </summary>
public record AssignDeviceToRoomCommand(int DeviceId, string Room);
