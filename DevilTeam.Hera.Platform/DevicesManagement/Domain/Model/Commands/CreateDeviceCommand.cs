namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Commands;

/// <summary>
///     Link a new device to an account (US11 / US37).
/// </summary>
public record CreateDeviceCommand(
    string Name,
    string Type,
    string Room,
    string Code,
    int? OwnerId);
