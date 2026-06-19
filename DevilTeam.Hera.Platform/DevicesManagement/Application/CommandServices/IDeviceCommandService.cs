using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Commands;
using DevilTeam.Hera.Platform.Shared.Application.Model;

namespace DevilTeam.Hera.Platform.DevicesManagement.Application.CommandServices;

/// <summary>
///     Device command service: one handler per write story (granular).
/// </summary>
public interface IDeviceCommandService
{
    Task<Result<Device>> Handle(CreateDeviceCommand command, CancellationToken cancellationToken);
    Task<Result<Device>> Handle(UpdateDeviceNameCommand command, CancellationToken cancellationToken);
    Task<Result<Device>> Handle(AssignDeviceToRoomCommand command, CancellationToken cancellationToken);
    Task<Result<Device>> Handle(UpdateDeviceStatusCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(DeleteDeviceCommand command, CancellationToken cancellationToken);
}
