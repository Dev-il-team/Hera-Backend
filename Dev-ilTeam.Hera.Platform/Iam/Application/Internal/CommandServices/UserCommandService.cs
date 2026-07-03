using Dev_ilTeam.Hera.Platform.Iam.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Iam.Application.Internal.OutboundServices;
using Dev_ilTeam.Hera.Platform.Iam.Domain.Model;
using Dev_ilTeam.Hera.Platform.Iam.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Iam.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Iam.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;
using Dev_ilTeam.Hera.Platform.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Dev_ilTeam.Hera.Platform.Iam.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IUserCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;

    public async Task<Result<(User user, string token)>> Handle(SignInCommand command,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username, cancellationToken);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            return Result<(User user, string token)>.Failure(IamError.InvalidCredentials,
                _localizer[nameof(IamError.InvalidCredentials)]);

        var token = tokenService.GenerateToken(user);

        return Result<(User user, string token)>.Success((user, token));
    }

    public async Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByUsernameAsync(command.Username, cancellationToken))
            return Result.Failure(IamError.UsernameAlreadyTaken,
                _localizer[nameof(IamError.UsernameAlreadyTaken), command.Username]);

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword);
        try
        {
            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            return Result.Failure(IamError.OperationCancelled, _localizer[nameof(IamError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result.Failure(IamError.DatabaseError, _localizer[nameof(IamError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result.Failure(IamError.InternalServerError, _localizer[nameof(IamError.InternalServerError)]);
        }
    }
}
