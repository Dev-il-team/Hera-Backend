using Dev_ilTeam.Hera.Platform.Devices.Domain.Model;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Entities;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;
using Dev_ilTeam.Hera.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Dev_ilTeam.Hera.Platform.Devices.Interfaces.Rest.Transform;

public static class DevicesActionResultAssembler
{
    private static int ToStatusCodeFromDevicesError(DevicesError error)
    {
        return error switch
        {
            DevicesError.RoomNotFound => StatusCodes.Status404NotFound,
            DevicesError.DeviceNotFound => StatusCodes.Status404NotFound,
            DevicesError.DuplicateDeviceName => StatusCodes.Status409Conflict,
            DevicesError.OperationCancelled => StatusCodes.Status409Conflict,
            DevicesError.DatabaseError => StatusCodes.Status500InternalServerError,
            DevicesError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromCreateRoomResult(
        ControllerBase controller,
        Result<Room> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Room, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCodeFromDevicesError((DevicesError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetRoomByIdResult(
        ControllerBase controller,
        Room? room,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Room, IActionResult> successAction)
    {
        if (room is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromDevicesError(DevicesError.RoomNotFound),
                DevicesError.RoomNotFound,
                errorLocalizer[nameof(DevicesError.RoomNotFound)]);
        return successAction(room);
    }

    public static IActionResult ToActionResultFromRegisterDeviceResult(
        ControllerBase controller,
        Result<Device> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Device, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCodeFromDevicesError((DevicesError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
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
                ToStatusCodeFromDevicesError(DevicesError.DeviceNotFound),
                DevicesError.DeviceNotFound,
                errorLocalizer[nameof(DevicesError.DeviceNotFound)]);
        return successAction(device);
    }

    public static IActionResult ToActionResultFromGetAllDevicesByRoomIdResult(
        ControllerBase controller,
        IEnumerable<Device> devices,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<IEnumerable<Device>, IActionResult> successAction)
    {
        return successAction(devices);
    }
}
