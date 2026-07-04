using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Queries;

namespace Dev_ilTeam.Hera.Platform.Devices.Application.QueryServices;

public interface IDeviceQueryService
{
    Task<Device?> Handle(GetDeviceByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Device>> Handle(GetAllDevicesQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Device>> Handle(GetAllDevicesByRoomIdQuery query, CancellationToken cancellationToken);
}
