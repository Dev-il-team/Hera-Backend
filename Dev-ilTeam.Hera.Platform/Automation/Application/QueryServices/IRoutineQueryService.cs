using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Queries;

namespace Dev_ilTeam.Hera.Platform.Automation.Application.QueryServices;

public interface IRoutineQueryService
{
    Task<IEnumerable<Routine>> Handle(GetAllRoutinesQuery query, CancellationToken cancellationToken);
    Task<Routine?> Handle(GetRoutineByIdQuery query, CancellationToken cancellationToken);
}
