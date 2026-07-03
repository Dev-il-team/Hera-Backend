using Dev_ilTeam.Hera.Platform.Profiles.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Profiles.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;

namespace Dev_ilTeam.Hera.Platform.Profiles.Application.CommandServices;

public interface IProfileCommandService
{
    Task<Result<Profile>> Handle(CreateProfileCommand command, CancellationToken cancellationToken);
}
