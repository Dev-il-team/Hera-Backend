using System.Net.Mime;
using Dev_ilTeam.Hera.Platform.Automation.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Automation.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Queries;
using Dev_ilTeam.Hera.Platform.Automation.Interfaces.Rest.Resources;
using Dev_ilTeam.Hera.Platform.Automation.Interfaces.Rest.Transform;
using Dev_ilTeam.Hera.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace Dev_ilTeam.Hera.Platform.Automation.Interfaces.Rest;


[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Routine Endpoints.")]
public class RoutinesController(
    IRoutineCommandService routineCommandService,
    IRoutineQueryService routineQueryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet("{routineId:int}")]
    [SwaggerOperation("Get Routine by Id", "Get a routine by its unique identifier.", OperationId = "GetRoutineById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The routine was found.", typeof(RoutineResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The routine was not found.")]
    public async Task<IActionResult> GetRoutineById(int routineId, CancellationToken cancellationToken)
    {
        var query = new GetRoutineByIdQuery(routineId);
        var routine = await routineQueryService.Handle(query, cancellationToken);

        return AutomationActionResultAssembler.ToActionResultFromGetRoutineByIdResult(
            this,
            routine,
            _errorLocalizer,
            _problemDetailsFactory,
            found => Ok(RoutineResourceFromEntityAssembler.ToResourceFromEntity(found)));
    }

    [HttpPost]
    [SwaggerOperation("Create Routine", "Program a new automation routine.", OperationId = "CreateRoutine")]
    [SwaggerResponse(StatusCodes.Status201Created, "The routine was created.", typeof(RoutineResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The routine could not be created.")]
    public async Task<IActionResult> CreateRoutine([FromBody] CreateRoutineResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateRoutineCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await routineCommandService.Handle(command, cancellationToken);

        return AutomationActionResultAssembler.ToActionResultFromCreateRoutineResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            created => CreatedAtAction(nameof(GetRoutineById), new { routineId = created.Id },
                RoutineResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation("Get All Routines", "List all programmed routines.", OperationId = "GetAllRoutines")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of routines.", typeof(IEnumerable<RoutineResource>))]
    public async Task<IActionResult> GetAllRoutines(CancellationToken cancellationToken)
    {
        var query = new GetAllRoutinesQuery();
        var routines = await routineQueryService.Handle(query, cancellationToken);
        return Ok(routines.Select(RoutineResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
