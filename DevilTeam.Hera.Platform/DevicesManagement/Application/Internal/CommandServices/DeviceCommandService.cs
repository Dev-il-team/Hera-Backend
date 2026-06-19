using DevilTeam.Hera.Platform.DevicesManagement.Application.CommandServices;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Commands;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Repositories;
using DevilTeam.Hera.Platform.Shared.Application.Model;
using DevilTeam.Hera.Platform.Shared.Domain.Repositories;

namespace DevilTeam.Hera.Platform.DevicesManagement.Application.Internal.CommandServices;

/// <summary>
///     Device command service.
/// </summary>
public class DeviceCommandService(
    IDeviceRepository deviceRepository,
    IUnitOfWork unitOfWork)
    : IDeviceCommandService
{
    public async Task<Result<Device>> Handle(CreateDeviceCommand command, CancellationToken cancellationToken)
    {
        var device = new Device(command);

        await deviceRepository.AddAsync(device, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<Device>.Success(device);
    }
}