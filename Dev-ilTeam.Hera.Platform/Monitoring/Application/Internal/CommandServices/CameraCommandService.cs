using Dev_ilTeam.Hera.Platform.Monitoring.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model;
using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;
using Dev_ilTeam.Hera.Platform.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Dev_ilTeam.Hera.Platform.Monitoring.Application.Internal.CommandServices;

/// <summary>
///     Represents the camera command service in the Hera Platform.
/// </summary>
/// <param name="cameraRepository">
///     The <see cref="ICameraRepository"/> to use
/// </param>
/// <param name="unitOfWork">
///     The <see cref="IUnitOfWork"/> to use.
/// </param>
public class CameraCommandService(
    ICameraRepository cameraRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : ICameraCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;

    public async Task<Result<Camera>> Handle(RegisterCameraCommand command, CancellationToken cancellationToken)
    {
        if (await cameraRepository.ExistsByNameAsync(command.Name, cancellationToken))
            return Result<Camera>.Failure(MonitoringError.DuplicateCameraName,
                _localizer[nameof(MonitoringError.DuplicateCameraName), command.Name]);
        var camera = new Camera(command);
        try
        {
            await cameraRepository.AddAsync(camera, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Camera>.Success(camera);
        }
        catch (OperationCanceledException)
        {
            return Result<Camera>.Failure(MonitoringError.OperationCancelled,
                _localizer[nameof(MonitoringError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Camera>.Failure(MonitoringError.DatabaseError,
                _localizer[nameof(MonitoringError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Camera>.Failure(MonitoringError.InternalServerError,
                _localizer[nameof(MonitoringError.InternalServerError)]);
        }
    }
}
