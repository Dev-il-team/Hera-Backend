using Dev_ilTeam.Hera.Platform.Devices.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Entities;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Queries;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Repositories;

namespace Dev_ilTeam.Hera.Platform.Devices.Application.Internal.QueryServices;

public class RoomQueryService(IRoomRepository roomRepository) : IRoomQueryService
{
    public async Task<IEnumerable<Room>> Handle(GetAllRoomsQuery query, CancellationToken cancellationToken)
    {
        return await roomRepository.ListAsync(cancellationToken);
    }

    public async Task<Room?> Handle(GetRoomByIdQuery query, CancellationToken cancellationToken)
    {
        return await roomRepository.FindByIdAsync(query.RoomId, cancellationToken);
    }
}
