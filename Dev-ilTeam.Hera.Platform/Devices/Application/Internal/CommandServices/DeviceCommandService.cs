using Dev_ilTeam.Hera.Platform.Devices.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;
using Dev_ilTeam.Hera.Platform.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Dev_ilTeam.Hera.Platform.Devices.Application.Internal.CommandServices;

public class DeviceCommandService(
    IDeviceRepository deviceRepository,
    IRoomRepository roomRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IDeviceCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;

    public async Task<Result<Device>> Handle(RegisterDeviceCommand command, CancellationToken cancellationToken)
    {
        var room = await roomRepository.FindByIdAsync(command.RoomId, cancellationToken);
        if (room is null)
            return Result<Device>.Failure(DevicesError.RoomNotFound, _localizer[nameof(DevicesError.RoomNotFound)]);
        if (await deviceRepository.ExistsByNameAsync(command.Name, cancellationToken))
            return Result<Device>.Failure(DevicesError.DuplicateDeviceName,
                _localizer[nameof(DevicesError.DuplicateDeviceName), command.Name]);
        var device = new Device(command);
        try
        {
            await deviceRepository.AddAsync(device, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            device.Room = room;
            return Result<Device>.Success(device);
        }
        catch (OperationCanceledException)
        {
            return Result<Device>.Failure(DevicesError.OperationCancelled,
                _localizer[nameof(DevicesError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Device>.Failure(DevicesError.DatabaseError, _localizer[nameof(DevicesError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Device>.Failure(DevicesError.InternalServerError,
                _localizer[nameof(DevicesError.InternalServerError)]);
        }
    }
}
