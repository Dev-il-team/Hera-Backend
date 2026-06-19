namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Queries;

/// <summary>
///     Query to get a device by id.
/// </summary>
/// <param name="Id">The device id.</param>
public record GetDeviceByIdQuery(int Id);