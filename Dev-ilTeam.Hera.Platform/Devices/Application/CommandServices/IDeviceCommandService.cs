using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;

namespace Dev_ilTeam.Hera.Platform.Devices.Application.CommandServices;

public interface IDeviceCommandService
{
    Task<Result<Device>> Handle(RegisterDeviceCommand command, CancellationToken cancellationToken);
}
