using Dev_ilTeam.Hera.Platform.Devices.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Queries;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Repositories;

namespace Dev_ilTeam.Hera.Platform.Devices.Application.Internal.QueryServices;

public class DeviceQueryService(IDeviceRepository deviceRepository) : IDeviceQueryService
{
    public async Task<Device?> Handle(GetDeviceByIdQuery query, CancellationToken cancellationToken)
    {
        return await deviceRepository.FindByIdAsync(query.DeviceId, cancellationToken);
    }

    public async Task<IEnumerable<Device>> Handle(GetAllDevicesQuery query, CancellationToken cancellationToken)
    {
        return await deviceRepository.ListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Device>> Handle(GetAllDevicesByRoomIdQuery query, CancellationToken cancellationToken)
    {
        return await deviceRepository.FindByRoomIdAsync(query.RoomId, cancellationToken);
    }
}
