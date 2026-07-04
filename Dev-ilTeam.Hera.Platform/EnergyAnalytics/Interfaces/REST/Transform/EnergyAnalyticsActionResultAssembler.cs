using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;
using Dev_ilTeam.Hera.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Interfaces.Rest.Transform;

public static class EnergyAnalyticsActionResultAssembler
{
    private static int ToStatusCodeFromEnergyAnalyticsError(EnergyAnalyticsError error)
    {
        return error switch
        {
            EnergyAnalyticsError.ConsumptionReportNotFound => StatusCodes.Status404NotFound,
            EnergyAnalyticsError.OperationCancelled => StatusCodes.Status409Conflict,
            EnergyAnalyticsError.DatabaseError => StatusCodes.Status500InternalServerError,
            EnergyAnalyticsError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromRecordConsumptionResult(
        ControllerBase controller,
        Result<ConsumptionReport> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<ConsumptionReport, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCodeFromEnergyAnalyticsError((EnergyAnalyticsError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetConsumptionReportByIdResult(
        ControllerBase controller,
        ConsumptionReport? report,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<ConsumptionReport, IActionResult> successAction)
    {
        if (report is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromEnergyAnalyticsError(EnergyAnalyticsError.ConsumptionReportNotFound),
                EnergyAnalyticsError.ConsumptionReportNotFound,
                errorLocalizer[nameof(EnergyAnalyticsError.ConsumptionReportNotFound)]);
        return successAction(report);
    }
}
