using System.Net.Mime;
using Dev_ilTeam.Hera.Platform.Devices.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Devices.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Queries;
using Dev_ilTeam.Hera.Platform.Devices.Interfaces.Rest.Resources;
using Dev_ilTeam.Hera.Platform.Devices.Interfaces.Rest.Transform;
using Dev_ilTeam.Hera.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace Dev_ilTeam.Hera.Platform.Devices.Interfaces.Rest;

[Authorize]
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
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet("{deviceId:int}")]
    [SwaggerOperation("Get Device by Id", "Get a device by its unique identifier.", OperationId = "GetDeviceById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The device was found.", typeof(DeviceResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The device was not found.")]
    public async Task<IActionResult> GetDeviceById(int deviceId, CancellationToken cancellationToken)
    {
        var getDeviceByIdQuery = new GetDeviceByIdQuery(deviceId);
        var device = await deviceQueryService.Handle(getDeviceByIdQuery, cancellationToken);

        return DevicesActionResultAssembler.ToActionResultFromGetDeviceByIdResult(
            this,
            device,
            _errorLocalizer,
            _problemDetailsFactory,
            foundDevice => Ok(DeviceResourceFromEntityAssembler.ToResourceFromEntity(foundDevice)));
    }

    [HttpPost]
    [SwaggerOperation("Register Device", "Link a new IoT device to the platform.", OperationId = "RegisterDevice")]
    [SwaggerResponse(StatusCodes.Status201Created, "The device was registered.", typeof(DeviceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The device could not be registered.")]
    public async Task<IActionResult> RegisterDevice([FromBody] RegisterDeviceResource resource,
        CancellationToken cancellationToken)
    {
        var registerDeviceCommand = RegisterDeviceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await deviceCommandService.Handle(registerDeviceCommand, cancellationToken);

        return DevicesActionResultAssembler.ToActionResultFromRegisterDeviceResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdDevice => CreatedAtAction(nameof(GetDeviceById), new { deviceId = createdDevice.Id },
                DeviceResourceFromEntityAssembler.ToResourceFromEntity(createdDevice)));
    }

    [HttpGet]
    [SwaggerOperation("Get All Devices", "Get all linked devices.", OperationId = "GetAllDevices")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of devices.", typeof(IEnumerable<DeviceResource>))]
    public async Task<IActionResult> GetAllDevices(CancellationToken cancellationToken)
    {
        var getAllDevicesQuery = new GetAllDevicesQuery();
        var devices = await deviceQueryService.Handle(getAllDevicesQuery, cancellationToken);
        var resources = devices.Select(DeviceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
