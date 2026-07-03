using Dev_ilTeam.Hera.Platform.Iam.Domain.Model.Aggregates;

namespace Dev_ilTeam.Hera.Platform.Iam.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);

    Task<int?> ValidateToken(string token);
}
