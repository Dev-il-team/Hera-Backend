using Dev_ilTeam.Hera.Platform.Iam.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Iam.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Iam.Domain.Model.Queries;
using Dev_ilTeam.Hera.Platform.Iam.Domain.Repositories;

namespace Dev_ilTeam.Hera.Platform.Iam.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        return await userRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        return await userRepository.ListAsync(cancellationToken);
    }

    public async Task<User?> Handle(GetUserByUsernameQuery query, CancellationToken cancellationToken)
    {
        return await userRepository.FindByUsernameAsync(query.Username, cancellationToken);
    }
}
