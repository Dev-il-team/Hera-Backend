namespace Dev_ilTeam.Hera.Platform.Iam.Application.Internal.OutboundServices;

public interface IHashingService
{
    string HashPassword(string password);

    bool VerifyPassword(string password, string passwordHash);
}
