using Dev_ilTeam.Hera.Platform.Automation.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Queries;
using Dev_ilTeam.Hera.Platform.Automation.Domain.Repositories;

namespace Dev_ilTeam.Hera.Platform.Automation.Application.Internal.QueryServices;

public class RoutineQueryService(IRoutineRepository routineRepository) : IRoutineQueryService
{
    public async Task<IEnumerable<Routine>> Handle(GetAllRoutinesQuery query, CancellationToken cancellationToken)
    {
        return await routineRepository.ListAsync(cancellationToken);
    }

    public async Task<Routine?> Handle(GetRoutineByIdQuery query, CancellationToken cancellationToken)
    {
        return await routineRepository.FindByIdAsync(query.RoutineId, cancellationToken);
    }
}
