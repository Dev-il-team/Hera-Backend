using Dev_ilTeam.Hera.Platform.Iam.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Shared.Domain.Repositories;

namespace Dev_ilTeam.Hera.Platform.Iam.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken);

    Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken);
}
