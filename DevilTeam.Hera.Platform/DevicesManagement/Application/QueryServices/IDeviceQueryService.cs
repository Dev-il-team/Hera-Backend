using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Queries;

namespace DevilTeam.Hera.Platform.DevicesManagement.Application.QueryServices;

/// <summary>
///     Device query service interface.
/// </summary>
public interface IDeviceQueryService
{
    Task<Device?> Handle(GetDeviceByIdQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<Device>> Handle(GetAllDevicesQuery query, CancellationToken cancellationToken);
}