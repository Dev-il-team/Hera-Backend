using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;
using DevilTeam.Hera.Platform.Resources.Errors;
using DevilTeam.Hera.Platform.Shared.Application.Model;
using DevilTeam.Hera.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Transform;

public static class DevicesActionResultAssembler
{
    private static int ToStatusCodeFromDevicesManagementError(DevicesManagementError error)
    {
        return error switch
        {
            DevicesManagementError.DeviceNotFound => StatusCodes.Status404NotFound,
            DevicesManagementError.DuplicateDeviceName => StatusCodes.Status409Conflict,
            DevicesManagementError.InvalidDeviceData => StatusCodes.Status400BadRequest,
            DevicesManagementError.OperationCancelled => StatusCodes.Status409Conflict,
            DevicesManagementError.DatabaseError => StatusCodes.Status500InternalServerError,
            DevicesManagementError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromCreateDeviceResult(
        ControllerBase controller,
        Result<Device> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Device, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromDevicesManagementError((DevicesManagementError)result.Error!);

        return problemDetailsFactory.CreateProblemDetails(
            controller,
            statusCode,
            result.Error,
            result.Message);
    }

    public static IActionResult ToActionResultFromGetDeviceByIdResult(
        ControllerBase controller,
        Device? device,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Device, IActionResult> successAction)
    {
        if (device is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromDevicesManagementError(DevicesManagementError.DeviceNotFound),
                DevicesManagementError.DeviceNotFound,
                errorLocalizer[nameof(DevicesManagementError.DeviceNotFound)]
            );

        return successAction(device);
    }
}