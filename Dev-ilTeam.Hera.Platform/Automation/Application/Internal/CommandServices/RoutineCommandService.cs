using Dev_ilTeam.Hera.Platform.Automation.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Automation.Domain.Model;
using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Automation.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;
using Dev_ilTeam.Hera.Platform.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Dev_ilTeam.Hera.Platform.Automation.Application.Internal.CommandServices;

public class RoutineCommandService(
    IRoutineRepository routineRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IRoutineCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;

    public async Task<Result<Routine>> Handle(CreateRoutineCommand command, CancellationToken cancellationToken)
    {
        if (await routineRepository.ExistsByNameAsync(command.Name, cancellationToken))
            return Result<Routine>.Failure(AutomationError.DuplicateRoutineName,
                _localizer[nameof(AutomationError.DuplicateRoutineName), command.Name]);
        var routine = new Routine(command);
        try
        {
            await routineRepository.AddAsync(routine, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Routine>.Success(routine);
        }
        catch (OperationCanceledException)
        {
            return Result<Routine>.Failure(AutomationError.OperationCancelled,
                _localizer[nameof(AutomationError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Routine>.Failure(AutomationError.DatabaseError,
                _localizer[nameof(AutomationError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Routine>.Failure(AutomationError.InternalServerError,
                _localizer[nameof(AutomationError.InternalServerError)]);
        }
    }
}
