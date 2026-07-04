using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Entities;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;

namespace Dev_ilTeam.Hera.Platform.Devices.Application.CommandServices;

public interface IRoomCommandService
{
    Task<Result<Room>> Handle(CreateRoomCommand command, CancellationToken cancellationToken);
}
