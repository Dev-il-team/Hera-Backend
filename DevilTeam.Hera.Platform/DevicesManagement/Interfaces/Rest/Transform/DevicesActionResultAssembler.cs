using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;
using DevilTeam.Hera.Platform.Resources.Errors;
using DevilTeam.Hera.Platform.Shared.Application.Model;
using DevilTeam.Hera.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Transform;

/// <summary>
///     Maps <see cref="DevicesError" /> outcomes to HTTP responses, mirroring the reference
///     platform's ActionResult assembler pattern.
/// </summary>
public static class DevicesActionResultAssembler
{
    private static int ToStatusCode(DevicesError error)
    {
        return error switch
        {
            DevicesError.DeviceNotFound => StatusCodes.Status404NotFound,
            DevicesError.InvalidDeviceData => StatusCodes.Status400BadRequest,
            DevicesError.OperationCancelled => StatusCodes.Status409Conflict,
            DevicesError.DatabaseError => StatusCodes.Status500InternalServerError,
            DevicesError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromCommandResult(
        ControllerBase controller,
        Result<Device> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Device, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCode((DevicesError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromDeleteResult(
        ControllerBase controller,
        Result result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction();

        var statusCode = ToStatusCode((DevicesError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetByIdResult(
        ControllerBase controller,
        Device? device,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Device, IActionResult> successAction)
    {
        if (device is not null) return successAction(device);

        return problemDetailsFactory.CreateProblemDetails(
            controller,
            ToStatusCode(DevicesError.DeviceNotFound),
            DevicesError.DeviceNotFound,
            errorLocalizer[nameof(DevicesError.DeviceNotFound)]);
    }
}
