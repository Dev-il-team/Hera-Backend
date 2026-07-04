using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Automation.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dev_ilTeam.Hera.Platform.Automation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class RoutineRepository(AppDbContext context) : BaseRepository<Routine>(context), IRoutineRepository
{
    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await Context.Set<Routine>().AnyAsync(routine => routine.Name == name, cancellationToken);
    }
}
