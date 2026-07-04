using System.Net.Mime;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Queries;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Interfaces.Rest.Resources;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Interfaces.Rest.Transform;
using Dev_ilTeam.Hera.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Consumption Report Endpoints.")]
public class ConsumptionReportsController(
    IConsumptionReportCommandService consumptionReportCommandService,
    IConsumptionReportQueryService consumptionReportQueryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet("{consumptionReportId:int}")]
    [SwaggerOperation("Get Consumption Report by Id", "Get a consumption report by its id.",
        OperationId = "GetConsumptionReportById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The report was found.", typeof(ConsumptionReportResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The report was not found.")]
    public async Task<IActionResult> GetConsumptionReportById(int consumptionReportId,
        CancellationToken cancellationToken)
    {
        var query = new GetConsumptionReportByIdQuery(consumptionReportId);
        var report = await consumptionReportQueryService.Handle(query, cancellationToken);

        return EnergyAnalyticsActionResultAssembler.ToActionResultFromGetConsumptionReportByIdResult(
            this,
            report,
            _errorLocalizer,
            _problemDetailsFactory,
            found => Ok(ConsumptionReportResourceFromEntityAssembler.ToResourceFromEntity(found)));
    }

    [HttpPost]
    [SwaggerOperation("Record Consumption", "Record a new energy consumption report.",
        OperationId = "RecordConsumption")]
    [SwaggerResponse(StatusCodes.Status201Created, "The report was recorded.", typeof(ConsumptionReportResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The report could not be recorded.")]
    public async Task<IActionResult> RecordConsumption([FromBody] RecordConsumptionResource resource,
        CancellationToken cancellationToken)
    {
        var command = RecordConsumptionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await consumptionReportCommandService.Handle(command, cancellationToken);

        return EnergyAnalyticsActionResultAssembler.ToActionResultFromRecordConsumptionResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            created => CreatedAtAction(nameof(GetConsumptionReportById), new { consumptionReportId = created.Id },
                ConsumptionReportResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [SwaggerOperation("Get All Consumption Reports", "List all consumption reports.",
        OperationId = "GetAllConsumptionReports")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of reports.", typeof(IEnumerable<ConsumptionReportResource>))]
    public async Task<IActionResult> GetAllConsumptionReports(CancellationToken cancellationToken)
    {
        var query = new GetAllConsumptionReportsQuery();
        var reports = await consumptionReportQueryService.Handle(query, cancellationToken);
        return Ok(reports.Select(ConsumptionReportResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
