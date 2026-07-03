using Dev_ilTeam.Hera.Platform.Profiles.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Profiles.Domain.Model.ValueObjects;
using Dev_ilTeam.Hera.Platform.Shared.Domain.Repositories;

namespace Dev_ilTeam.Hera.Platform.Profiles.Domain.Repositories;

public interface IProfileRepository : IBaseRepository<Profile>
{
    Task<Profile?> FindProfileByEmailAsync(EmailAddress email, CancellationToken cancellationToken);
}
