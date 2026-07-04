using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Queries;

namespace Dev_ilTeam.Hera.Platform.Monitoring.Application.QueryServices;

/// <summary>
///     Represents the camera query service in the Hera Platform.
/// </summary>
public interface ICameraQueryService
{
    /// <summary>
    ///     Handles the get all cameras query in the Hera Platform.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<Camera>> Handle(GetAllCamerasQuery query, CancellationToken cancellationToken);
    
    /// <summary>
    ///     Handles the get camera by id query in the Hera Platform.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Camera?> Handle(GetCameraByIdQuery query, CancellationToken cancellationToken);
}
