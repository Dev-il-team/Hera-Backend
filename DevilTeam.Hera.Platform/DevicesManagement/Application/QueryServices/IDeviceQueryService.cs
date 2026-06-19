using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Queries;

namespace DevilTeam.Hera.Platform.DevicesManagement.Application.QueryServices;

/// <summary>
///     Device query service.
/// </summary>
public interface IDeviceQueryService
{
    Task<IEnumerable<Device>> Handle(GetAllDevicesQuery query, CancellationToken cancellationToken);
    Task<Device?> Handle(GetDeviceByIdQuery query, CancellationToken cancellationToken);
}
