using Dev_ilTeam.Hera.Platform.Profiles.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Profiles.Domain.Model.Queries;

namespace Dev_ilTeam.Hera.Platform.Profiles.Application.QueryServices;

public interface IProfileQueryService
{
    Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query, CancellationToken cancellationToken);

    Task<Profile?> Handle(GetProfileByEmailQuery query, CancellationToken cancellationToken);

    Task<Profile?> Handle(GetProfileByIdQuery query, CancellationToken cancellationToken);
}
