namespace Dev_ilTeam.Hera.Platform.Monitoring.Interfaces.Rest.Resources;

/// <summary>
///     Register camera resource for REST API
/// </summary>
/// <param name="Name">
///     The name of the camera
/// </param>
/// <param name="Location">
///     The location of the camera
/// </param>
/// <param name="StreamUrl">
///     The URL for the stream of the camera
/// </param>
public record RegisterCameraResource(string Name, string Location, string StreamUrl);
