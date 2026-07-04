using Dev_ilTeam.Hera.Platform.Devices.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Entities;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Events;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;
using Dev_ilTeam.Hera.Platform.Shared.Domain.Repositories;
using Cortex.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Dev_ilTeam.Hera.Platform.Devices.Application.Internal.CommandServices;

public class RoomCommandService(
    IRoomRepository roomRepository,
    IUnitOfWork unitOfWork,
    IMediator domainEventPublisher,
    IStringLocalizer<ErrorMessages> localizer)
    : IRoomCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;

    public async Task<Result<Room>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        var room = new Room(command);
        try
        {
            await roomRepository.AddAsync(room, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            await domainEventPublisher.PublishAsync(new RoomCreatedEvent(room.Name), cancellationToken);
            return Result<Room>.Success(room);
        }
        catch (OperationCanceledException)
        {
            return Result<Room>.Failure(DevicesError.OperationCancelled,
                _localizer[nameof(DevicesError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Room>.Failure(DevicesError.DatabaseError, _localizer[nameof(DevicesError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Room>.Failure(DevicesError.InternalServerError,
                _localizer[nameof(DevicesError.InternalServerError)]);
        }
    }
}
