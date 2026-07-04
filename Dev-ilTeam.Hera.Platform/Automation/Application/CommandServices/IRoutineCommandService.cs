using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;

namespace Dev_ilTeam.Hera.Platform.Automation.Application.CommandServices;

public interface IRoutineCommandService
{
    Task<Result<Routine>> Handle(CreateRoutineCommand command, CancellationToken cancellationToken);
}
