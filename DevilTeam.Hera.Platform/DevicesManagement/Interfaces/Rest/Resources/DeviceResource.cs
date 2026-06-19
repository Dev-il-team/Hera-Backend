namespace DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Resources;

/// <summary>
///     Device representation returned by the API.
/// </summary>
public record DeviceResource(
    int Id,
    string Name,
    string Type,
    string Room,
    bool IsOn,
    string Connectivity,
    string Code,
    int? OwnerId,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
