namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.ValueObjects;

/// <summary>
///     Whether a device is currently reachable (US22). Independent of its power state.
/// </summary>
public enum ConnectivityStatus
{
    Offline,
    Online
}
