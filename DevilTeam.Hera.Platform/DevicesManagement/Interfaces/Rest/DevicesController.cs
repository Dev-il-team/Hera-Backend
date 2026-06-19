using System.Net.Mime;
using DevilTeam.Hera.Platform.DevicesManagement.Application.CommandServices;
using DevilTeam.Hera.Platform.DevicesManagement.Application.QueryServices;
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
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet("{deviceId:int}")]
    [SwaggerOperation("Get Device by Id", "Get a device by its unique identifier.", OperationId = "GetDeviceById")]
    [SwaggerResponse(200, "The device was found and returned.", typeof(DeviceResource))]
    [SwaggerResponse(404, "The device was not found.")]
    public async Task<IActionResult> GetDeviceById(int deviceId, CancellationToken cancellationToken)
    {
        var getDeviceByIdQuery = new GetDeviceByIdQuery(deviceId);
        var device = await deviceQueryService.Handle(getDeviceByIdQuery, cancellationToken);

        return DevicesActionResultAssembler.ToActionResultFromGetDeviceByIdResult(
            this,
            device,
            _errorLocalizer,
            _problemDetailsFactory,
            foundDevice => Ok(DeviceResourceFromEntityAssembler.ToResourceFromEntity(foundDevice))
        );
    }

    [HttpPost]
    [SwaggerOperation("Create Device", "Create a new device.", OperationId = "CreateDevice")]
    [SwaggerResponse(201, "The device was created.", typeof(DeviceResource))]
    [SwaggerResponse(400, "The device was not created.")]
    public async Task<IActionResult> CreateDevice(CreateDeviceResource resource, CancellationToken cancellationToken)
    {
        var createDeviceCommand = CreateDeviceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await deviceCommandService.Handle(createDeviceCommand, cancellationToken);

        return DevicesActionResultAssembler.ToActionResultFromCreateDeviceResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdDevice => CreatedAtAction(nameof(GetDeviceById), new { deviceId = createdDevice.Id },
                DeviceResourceFromEntityAssembler.ToResourceFromEntity(createdDevice))
        );
    }

    [HttpGet]
    [SwaggerOperation("Get All Devices", "Get all devices.", OperationId = "GetAllDevices")]
    [SwaggerResponse(200, "The devices were found and returned.", typeof(IEnumerable<DeviceResource>))]
    [SwaggerResponse(404, "The devices were not found.")]
    public async Task<IActionResult> GetAllDevices(CancellationToken cancellationToken)
    {
        var getAllDevicesQuery = new GetAllDevicesQuery();
        var devices = await deviceQueryService.Handle(getAllDevicesQuery, cancellationToken);
        var deviceResources = devices.Select(DeviceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(deviceResources);
    }
}