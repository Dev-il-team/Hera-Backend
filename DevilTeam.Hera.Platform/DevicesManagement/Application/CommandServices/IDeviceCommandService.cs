using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Commands;
using DevilTeam.Hera.Platform.Shared.Application.Model;

namespace DevilTeam.Hera.Platform.DevicesManagement.Application.CommandServices;

/// <summary>
///     Device command service interface.
/// </summary>
public interface IDeviceCommandService
{
    /// <summary>
    ///     Handle create device command.
    /// </summary>
    /// <param name="command">The create device command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created device.</returns>
    Task<Result<Device>> Handle(CreateDeviceCommand command, CancellationToken cancellationToken);
}