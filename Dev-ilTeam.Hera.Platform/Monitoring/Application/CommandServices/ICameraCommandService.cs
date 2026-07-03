using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;

namespace Dev_ilTeam.Hera.Platform.Monitoring.Application.CommandServices;

/// <summary>
///     Represents the camera command service in the Hera Platform.
/// </summary>
public interface ICameraCommandService
{
    /// <summary>
    ///     Handles the register camera command in the Hera Platform.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="RegisterCameraCommand"/> command to handle
    /// </param>
    /// <param name="cancellationToken"> The cancellation token </param>
    /// <returns>
    ///     The created <see cref="Camera"/> entity.
    /// </returns>
    Task<Result<Camera>> Handle(RegisterCameraCommand command, CancellationToken cancellationToken);
}
