using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Shared.Domain.Repositories;

namespace Dev_ilTeam.Hera.Platform.Automation.Domain.Repositories;

public interface IRoutineRepository : IBaseRepository<Routine>
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
}
