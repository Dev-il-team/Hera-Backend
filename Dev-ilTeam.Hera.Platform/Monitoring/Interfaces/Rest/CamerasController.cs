using System.Net.Mime;
using Dev_ilTeam.Hera.Platform.Monitoring.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Monitoring.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Queries;
using Dev_ilTeam.Hera.Platform.Monitoring.Interfaces.Rest.Resources;
using Dev_ilTeam.Hera.Platform.Monitoring.Interfaces.Rest.Transform;
using Dev_ilTeam.Hera.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace Dev_ilTeam.Hera.Platform.Monitoring.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Camera Endpoints.")]
public class CamerasController(
    ICameraCommandService cameraCommandService,
    ICameraQueryService cameraQueryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet("{cameraId:int}")]
    [SwaggerOperation("Get Camera by Id", "Get a camera by its unique identifier.", OperationId = "GetCameraById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The camera was found.", typeof(CameraResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The camera was not found.")]
    public async Task<IActionResult> GetCameraById(int cameraId, CancellationToken cancellationToken)
    {
        var query = new GetCameraByIdQuery(cameraId);
        var camera = await cameraQueryService.Handle(query, cancellationToken);

        return MonitoringActionResultAssembler.ToActionResultFromGetCameraByIdResult(
            this,
            camera,
            _errorLocalizer,
            _problemDetailsFactory,
            found => Ok(CameraResourceFromEntityAssembler.ToResourceFromEntity(found)));
    }

    [HttpPost]
    [SwaggerOperation("Register Camera", "Register a surveillance camera.", OperationId = "RegisterCamera")]
    [SwaggerResponse(StatusCodes.Status201Created, "The camera was registered.", typeof(CameraResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The camera could not be registered.")]
    public async Task<IActionResult> RegisterCamera([FromBody] RegisterCameraResource resource,
        CancellationToken cancellationToken)
    {
        var command = RegisterCameraCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await cameraCommandService.Handle(command, cancellationToken);

        return MonitoringActionResultAssembler.ToActionResultFromRegisterCameraResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            created => CreatedAtAction(nameof(GetCameraById), new { cameraId = created.Id },
                CameraResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [SwaggerOperation("Get All Cameras", "List all surveillance cameras.", OperationId = "GetAllCameras")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of cameras.", typeof(IEnumerable<CameraResource>))]
    public async Task<IActionResult> GetAllCameras(CancellationToken cancellationToken)
    {
        var query = new GetAllCamerasQuery();
        var cameras = await cameraQueryService.Handle(query, cancellationToken);
        return Ok(cameras.Select(CameraResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
