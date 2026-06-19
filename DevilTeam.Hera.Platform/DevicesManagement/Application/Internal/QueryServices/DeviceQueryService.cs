using DevilTeam.Hera.Platform.DevicesManagement.Application.QueryServices;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Queries;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Repositories;

namespace DevilTeam.Hera.Platform.DevicesManagement.Application.Internal.QueryServices;

/// <summary>
///     Device query service.
/// </summary>
public class DeviceQueryService(IDeviceRepository deviceRepository) : IDeviceQueryService
{
    public async Task<Device?> Handle(GetDeviceByIdQuery query, CancellationToken cancellationToken)
    {
        return await deviceRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<Device>> Handle(GetAllDevicesQuery query, CancellationToken cancellationToken)
    {
        return await deviceRepository.ListAsync(cancellationToken);
    }
}