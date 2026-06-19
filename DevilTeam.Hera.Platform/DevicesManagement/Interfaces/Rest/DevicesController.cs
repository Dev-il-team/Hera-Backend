using System.Net.Mime;
using DevilTeam.Hera.Platform.DevicesManagement.Application.CommandServices;
using DevilTeam.Hera.Platform.DevicesManagement.Application.QueryServices;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Commands;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Queries;
using DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Resources;
using DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest.Transform;
using DevilTeam.Hera.Platform.Resources.Errors;
using DevilTeam.Hera.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace DevilTeam.Hera.Platform.DevicesManagement.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Device Endpoints.")]
public class DevicesController(
    IDeviceCommandService deviceCommandService,
    IDeviceQueryService deviceQueryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All Devices", "List all linked devices.", OperationId = "GetAllDevices")]
    [SwaggerResponse(200, "The devices were returned.", typeof(IEnumerable<DeviceResource>))]
    public async Task<IActionResult> GetAllDevices(CancellationToken cancellationToken)
    {
        var devices = await deviceQueryService.Handle(new GetAllDevicesQuery(), cancellationToken);
        return Ok(devices.Select(DeviceResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{deviceId:int}")]
    [SwaggerOperation("Get Device by Id", "Get a device by its unique identifier.", OperationId = "GetDeviceById")]
    [SwaggerResponse(200, "The device was found and returned.", typeof(DeviceResource))]
    [SwaggerResponse(404, "The device was not found.")]
    public async Task<IActionResult> GetDeviceById(int deviceId, CancellationToken cancellationToken)
    {
        var device = await deviceQueryService.Handle(new GetDeviceByIdQuery(deviceId), cancellationToken);
        return DevicesActionResultAssembler.ToActionResultFromGetByIdResult(
            this, device, errorLocalizer, problemDetailsFactory,
            found => Ok(DeviceResourceFromEntityAssembler.ToResourceFromEntity(found)));
    }

    [HttpPost]
    [SwaggerOperation("Create Device", "Link a new device to an account.", OperationId = "CreateDevice")]
    [SwaggerResponse(201, "The device was created.", typeof(DeviceResource))]
    [SwaggerResponse(400, "The device was not created.")]
    public async Task<IActionResult> CreateDevice(CreateDeviceResource resource, CancellationToken cancellationToken)
    {
        var command = CreateDeviceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await deviceCommandService.Handle(command, cancellationToken);
        return DevicesActionResultAssembler.ToActionResultFromCommandResult(
            this, result, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetDeviceById), new { deviceId = created.Id },
                DeviceResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpPatch("{deviceId:int}/name")]
    [SwaggerOperation("Rename Device", "Change a device's display name.", OperationId = "RenameDevice")]
    [SwaggerResponse(200, "The device was renamed.", typeof(DeviceResource))]
    [SwaggerResponse(400, "The new name was invalid.")]
    [SwaggerResponse(404, "The device was not found.")]
    public async Task<IActionResult> RenameDevice(int deviceId, UpdateDeviceNameResource resource,
        CancellationToken cancellationToken)
    {
        var result = await deviceCommandService.Handle(
            new UpdateDeviceNameCommand(deviceId, resource.Name), cancellationToken);
        return DevicesActionResultAssembler.ToActionResultFromCommandResult(
            this, result, problemDetailsFactory,
            updated => Ok(DeviceResourceFromEntityAssembler.ToResourceFromEntity(updated)));
    }

    [HttpPatch("{deviceId:int}/room")]
    [SwaggerOperation("Assign Device Room", "Assign a device to a room/zone.", OperationId = "AssignDeviceRoom")]
    [SwaggerResponse(200, "The device was assigned to the room.", typeof(DeviceResource))]
    [SwaggerResponse(404, "The device was not found.")]
    public async Task<IActionResult> AssignDeviceRoom(int deviceId, AssignDeviceRoomResource resource,
        CancellationToken cancellationToken)
    {
        var result = await deviceCommandService.Handle(
            new AssignDeviceToRoomCommand(deviceId, resource.Room), cancellationToken);
        return DevicesActionResultAssembler.ToActionResultFromCommandResult(
            this, result, problemDetailsFactory,
            updated => Ok(DeviceResourceFromEntityAssembler.ToResourceFromEntity(updated)));
    }

    [HttpPatch("{deviceId:int}/status")]
    [SwaggerOperation("Set Device Status", "Turn a device on or off.", OperationId = "SetDeviceStatus")]
    [SwaggerResponse(200, "The device status was updated.", typeof(DeviceResource))]
    [SwaggerResponse(404, "The device was not found.")]
    public async Task<IActionResult> SetDeviceStatus(int deviceId, UpdateDeviceStatusResource resource,
        CancellationToken cancellationToken)
    {
        var result = await deviceCommandService.Handle(
            new UpdateDeviceStatusCommand(deviceId, resource.IsOn), cancellationToken);
        return DevicesActionResultAssembler.ToActionResultFromCommandResult(
            this, result, problemDetailsFactory,
            updated => Ok(DeviceResourceFromEntityAssembler.ToResourceFromEntity(updated)));
    }

    [HttpDelete("{deviceId:int}")]
    [SwaggerOperation("Unlink Device", "Unlink (delete) a device.", OperationId = "DeleteDevice")]
    [SwaggerResponse(204, "The device was unlinked.")]
    [SwaggerResponse(404, "The device was not found.")]
    public async Task<IActionResult> DeleteDevice(int deviceId, CancellationToken cancellationToken)
    {
        var result = await deviceCommandService.Handle(new DeleteDeviceCommand(deviceId), cancellationToken);
        return DevicesActionResultAssembler.ToActionResultFromDeleteResult(
            this, result, problemDetailsFactory, NoContent);
    }
}
