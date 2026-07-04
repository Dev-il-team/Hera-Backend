using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Shared.Domain.Repositories;

namespace Dev_ilTeam.Hera.Platform.Monitoring.Domain.Repositories;

public interface ICameraRepository : IBaseRepository<Camera>
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
}
