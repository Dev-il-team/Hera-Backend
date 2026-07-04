namespace Dev_ilTeam.Hera.Platform.Monitoring.Interfaces.Rest.Resources;

/// <summary>
///     Camera resource for REST API.
/// </summary>
/// <param name="Id">
///     The unique identifier of the camera
/// </param>
/// <param name="Name">
///     The name of the camera
/// </param>
/// <param name="Location">
///     The location of the camera
/// </param>
/// <param name="StreamUrl">
///     The URL for the stream of the camera
/// </param>
/// <param name="Status">
///     The status of the camera
/// </param>
public record CameraResource(int Id, string Name, string Location, string StreamUrl, string Status);
