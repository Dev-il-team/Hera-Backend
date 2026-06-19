using DevilTeam.Hera.Platform.DevicesManagement.Application.CommandServices;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Commands;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Repositories;
using DevilTeam.Hera.Platform.Resources.Errors;
using DevilTeam.Hera.Platform.Shared.Application.Model;
using DevilTeam.Hera.Platform.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DevilTeam.Hera.Platform.DevicesManagement.Application.Internal.CommandServices;

/// <summary>
///     Device command service. Domain validation surfaces as <see cref="DevicesError.InvalidDeviceData" />;
///     persistence faults map to database/internal errors via the <see cref="Result{T}" /> pipeline.
/// </summary>
public class DeviceCommandService(
    IDeviceRepository deviceRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IDeviceCommandService
{
    public async Task<Result<Device>> Handle(CreateDeviceCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var device = new Device(command);
            await deviceRepository.AddAsync(device, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Device>.Success(device);
        }
        catch (ArgumentException exception)
        {
            return Failure<Device>(DevicesError.InvalidDeviceData, exception.Message);
        }
        catch (OperationCanceledException)
        {
            return LocalizedFailure<Device>(DevicesError.OperationCancelled);
        }
        catch (DbUpdateException)
        {
            return LocalizedFailure<Device>(DevicesError.DatabaseError);
        }
        catch (Exception)
        {
            return LocalizedFailure<Device>(DevicesError.InternalServerError);
        }
    }

    public async Task<Result<Device>> Handle(UpdateDeviceNameCommand command, CancellationToken cancellationToken)
    {
        return await Mutate(command.DeviceId, device => device.Rename(command.Name), cancellationToken);
    }

    public async Task<Result<Device>> Handle(AssignDeviceToRoomCommand command, CancellationToken cancellationToken)
    {
        return await Mutate(command.DeviceId, device => device.AssignToRoom(command.Room), cancellationToken);
    }

    public async Task<Result<Device>> Handle(UpdateDeviceStatusCommand command, CancellationToken cancellationToken)
    {
        return await Mutate(command.DeviceId, device => device.SetPowerState(command.IsOn), cancellationToken);
    }

    public async Task<Result> Handle(DeleteDeviceCommand command, CancellationToken cancellationToken)
    {
        var device = await deviceRepository.FindByIdAsync(command.DeviceId, cancellationToken);
        if (device is null)
            return Result.Failure(DevicesError.DeviceNotFound, localizer[nameof(DevicesError.DeviceNotFound)]);

        try
        {
            deviceRepository.Remove(device);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            return Result.Failure(DevicesError.OperationCancelled, localizer[nameof(DevicesError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result.Failure(DevicesError.DatabaseError, localizer[nameof(DevicesError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result.Failure(DevicesError.InternalServerError, localizer[nameof(DevicesError.InternalServerError)]);
        }
    }

    private async Task<Result<Device>> Mutate(int deviceId, Action<Device> mutation, CancellationToken cancellationToken)
    {
        var device = await deviceRepository.FindByIdAsync(deviceId, cancellationToken);
        if (device is null)
            return LocalizedFailure<Device>(DevicesError.DeviceNotFound);

        try
        {
            mutation(device);
            deviceRepository.Update(device);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Device>.Success(device);
        }
        catch (ArgumentException exception)
        {
            return Failure<Device>(DevicesError.InvalidDeviceData, exception.Message);
        }
        catch (OperationCanceledException)
        {
            return LocalizedFailure<Device>(DevicesError.OperationCancelled);
        }
        catch (DbUpdateException)
        {
            return LocalizedFailure<Device>(DevicesError.DatabaseError);
        }
        catch (Exception)
        {
            return LocalizedFailure<Device>(DevicesError.InternalServerError);
        }
    }

    private Result<T> LocalizedFailure<T>(DevicesError error)
    {
        return Result<T>.Failure(error, localizer[error.ToString()]);
    }

    private static Result<T> Failure<T>(DevicesError error, string message)
    {
        return Result<T>.Failure(error, message);
    }
}
