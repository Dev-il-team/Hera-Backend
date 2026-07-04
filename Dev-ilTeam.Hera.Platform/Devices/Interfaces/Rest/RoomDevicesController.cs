using System.Net.Mime;
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
[Route("api/v1/rooms/{roomId:int}/devices")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Rooms")]
public class RoomDevicesController(
    IDeviceQueryService deviceQueryService,
    ProblemDetailsFactory problemDetailsFactory,
    IStringLocalizer<ErrorMessages> errorLocalizer)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet]
    [SwaggerOperation("Get all devices by room id", "Get all devices grouped by room.",
        OperationId = "GetAllDevicesByRoomId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of devices.", typeof(IEnumerable<DeviceResource>))]
    public async Task<IActionResult> GetDevicesByRoomId(int roomId, CancellationToken cancellationToken)
    {
        var query = new GetAllDevicesByRoomIdQuery(roomId);
        var devices = await deviceQueryService.Handle(query, cancellationToken);

        return DevicesActionResultAssembler.ToActionResultFromGetAllDevicesByRoomIdResult(
            this,
            devices,
            _errorLocalizer,
            _problemDetailsFactory,
            found => Ok(found.Select(DeviceResourceFromEntityAssembler.ToResourceFromEntity)));
    }
}
