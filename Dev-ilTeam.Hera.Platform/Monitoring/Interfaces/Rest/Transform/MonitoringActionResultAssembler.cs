using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model;
using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;
using Dev_ilTeam.Hera.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Dev_ilTeam.Hera.Platform.Monitoring.Interfaces.Rest.Transform;

public static class MonitoringActionResultAssembler
{
    private static int ToStatusCodeFromMonitoringError(MonitoringError error)
    {
        return error switch
        {
            MonitoringError.CameraNotFound => StatusCodes.Status404NotFound,
            MonitoringError.DuplicateCameraName => StatusCodes.Status409Conflict,
            MonitoringError.OperationCancelled => StatusCodes.Status409Conflict,
            MonitoringError.DatabaseError => StatusCodes.Status500InternalServerError,
            MonitoringError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromRegisterCameraResult(
        ControllerBase controller,
        Result<Camera> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Camera, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCodeFromMonitoringError((MonitoringError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetCameraByIdResult(
        ControllerBase controller,
        Camera? camera,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Camera, IActionResult> successAction)
    {
        if (camera is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromMonitoringError(MonitoringError.CameraNotFound),
                MonitoringError.CameraNotFound,
                errorLocalizer[nameof(MonitoringError.CameraNotFound)]);
        return successAction(camera);
    }
}
