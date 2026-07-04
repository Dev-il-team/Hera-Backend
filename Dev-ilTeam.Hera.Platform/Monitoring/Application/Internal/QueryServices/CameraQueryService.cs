using Dev_ilTeam.Hera.Platform.Monitoring.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Queries;
using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Repositories;

namespace Dev_ilTeam.Hera.Platform.Monitoring.Application.Internal.QueryServices;

/// <summary>
///     Represents the camera query service in the Hera Platform.
/// </summary>
/// <param name="cameraRepository"></param>
public class CameraQueryService(ICameraRepository cameraRepository) : ICameraQueryService
{
    public async Task<IEnumerable<Camera>> Handle(GetAllCamerasQuery query, CancellationToken cancellationToken)
    {
        return await cameraRepository.ListAsync(cancellationToken);
    }

    public async Task<Camera?> Handle(GetCameraByIdQuery query, CancellationToken cancellationToken)
    {
        return await cameraRepository.FindByIdAsync(query.CameraId, cancellationToken);
    }
}
