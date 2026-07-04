using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Entities;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Queries;

namespace Dev_ilTeam.Hera.Platform.Devices.Application.QueryServices;

public interface IRoomQueryService
{
    Task<IEnumerable<Room>> Handle(GetAllRoomsQuery query, CancellationToken cancellationToken);
    Task<Room?> Handle(GetRoomByIdQuery query, CancellationToken cancellationToken);
}
