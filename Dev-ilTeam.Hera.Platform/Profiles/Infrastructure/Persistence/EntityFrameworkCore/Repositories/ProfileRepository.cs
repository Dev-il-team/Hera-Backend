using Dev_ilTeam.Hera.Platform.Profiles.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Profiles.Domain.Model.ValueObjects;
using Dev_ilTeam.Hera.Platform.Profiles.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dev_ilTeam.Hera.Platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ProfileRepository(AppDbContext context)
    : BaseRepository<Profile>(context), IProfileRepository
{
    public async Task<Profile?> FindProfileByEmailAsync(EmailAddress email, CancellationToken cancellationToken)
    {
        return await Context.Set<Profile>().FirstOrDefaultAsync(p => p.Email == email, cancellationToken);
    }
}
