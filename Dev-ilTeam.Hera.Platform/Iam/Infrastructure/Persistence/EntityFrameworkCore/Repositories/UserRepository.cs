using Dev_ilTeam.Hera.Platform.Iam.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Iam.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dev_ilTeam.Hera.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Username.Equals(username), cancellationToken);
    }

    public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await Context.Set<User>().AnyAsync(user => user.Username.Equals(username), cancellationToken);
    }
}
