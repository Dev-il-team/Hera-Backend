using Dev_ilTeam.Hera.Platform.Iam.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Iam.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;

namespace Dev_ilTeam.Hera.Platform.Iam.Application.CommandServices;

public interface IUserCommandService
{
    Task<Result<(User user, string token)>> Handle(SignInCommand command, CancellationToken cancellationToken);

    Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken);
}
