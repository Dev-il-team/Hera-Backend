using Dev_ilTeam.Hera.Platform.Automation.Domain.Model;
using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;
using Dev_ilTeam.Hera.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Dev_ilTeam.Hera.Platform.Automation.Interfaces.Rest.Transform;

public static class AutomationActionResultAssembler
{
    private static int ToStatusCodeFromAutomationError(AutomationError error)
    {
        return error switch
        {
            AutomationError.RoutineNotFound => StatusCodes.Status404NotFound,
            AutomationError.DuplicateRoutineName => StatusCodes.Status409Conflict,
            AutomationError.OperationCancelled => StatusCodes.Status409Conflict,
            AutomationError.DatabaseError => StatusCodes.Status500InternalServerError,
            AutomationError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromCreateRoutineResult(
        ControllerBase controller,
        Result<Routine> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Routine, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCodeFromAutomationError((AutomationError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetRoutineByIdResult(
        ControllerBase controller,
        Routine? routine,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Routine, IActionResult> successAction)
    {
        if (routine is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromAutomationError(AutomationError.RoutineNotFound),
                AutomationError.RoutineNotFound,
                errorLocalizer[nameof(AutomationError.RoutineNotFound)]);
        return successAction(routine);
    }
}
