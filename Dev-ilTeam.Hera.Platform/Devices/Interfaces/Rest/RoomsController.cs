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
[SwaggerTag("Available Room Endpoints.")]
public class RoomsController(
    IRoomCommandService roomCommandService,
    IRoomQueryService roomQueryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet("{roomId:int}")]
    [SwaggerOperation("Get Room by Id", "Get a room by its unique identifier.", OperationId = "GetRoomById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The room was found.", typeof(RoomResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The room was not found.")]
    public async Task<IActionResult> GetRoomById(int roomId, CancellationToken cancellationToken)
    {
        var getRoomByIdQuery = new GetRoomByIdQuery(roomId);
        var room = await roomQueryService.Handle(getRoomByIdQuery, cancellationToken);

        return DevicesActionResultAssembler.ToActionResultFromGetRoomByIdResult(
            this,
            room,
            _errorLocalizer,
            _problemDetailsFactory,
            foundRoom => Ok(RoomResourceFromEntityAssembler.ToResourceFromEntity(foundRoom)));
    }

    [HttpPost]
    [SwaggerOperation("Create Room", "Create a new room.", OperationId = "CreateRoom")]
    [SwaggerResponse(StatusCodes.Status201Created, "The room was created.", typeof(RoomResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The room could not be created.")]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomResource resource,
        CancellationToken cancellationToken)
    {
        var createRoomCommand = CreateRoomCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await roomCommandService.Handle(createRoomCommand, cancellationToken);

        return DevicesActionResultAssembler.ToActionResultFromCreateRoomResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdRoom => CreatedAtAction(nameof(GetRoomById), new { roomId = createdRoom.Id },
                RoomResourceFromEntityAssembler.ToResourceFromEntity(createdRoom)));
    }

    [HttpGet]
    [SwaggerOperation("Get All Rooms", "Get all rooms.", OperationId = "GetAllRooms")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of rooms.", typeof(IEnumerable<RoomResource>))]
    public async Task<IActionResult> GetAllRooms(CancellationToken cancellationToken)
    {
        var getAllRoomsQuery = new GetAllRoomsQuery();
        var rooms = await roomQueryService.Handle(getAllRoomsQuery, cancellationToken);
        var resources = rooms.Select(RoomResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
