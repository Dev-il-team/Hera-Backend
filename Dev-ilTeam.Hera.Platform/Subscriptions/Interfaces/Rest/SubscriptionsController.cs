using System.Net.Mime;
using Dev_ilTeam.Hera.Platform.Subscriptions.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Subscriptions.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Queries;
using Dev_ilTeam.Hera.Platform.Subscriptions.Interfaces.Rest.Resources;
using Dev_ilTeam.Hera.Platform.Subscriptions.Interfaces.Rest.Transform;
using Dev_ilTeam.Hera.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace Dev_ilTeam.Hera.Platform.Subscriptions.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Subscription Endpoints.")]
public class SubscriptionsController(
    ISubscriptionCommandService subscriptionCommandService,
    ISubscriptionQueryService subscriptionQueryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet("{subscriptionId:int}")]
    [SwaggerOperation("Get Subscription by Id", "Get a subscription by its id.", OperationId = "GetSubscriptionById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The subscription was found.", typeof(SubscriptionResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The subscription was not found.")]
    public async Task<IActionResult> GetSubscriptionById(int subscriptionId, CancellationToken cancellationToken)
    {
        var query = new GetSubscriptionByIdQuery(subscriptionId);
        var subscription = await subscriptionQueryService.Handle(query, cancellationToken);

        return SubscriptionsActionResultAssembler.ToActionResultFromGetSubscriptionByIdResult(
            this,
            subscription,
            _errorLocalizer,
            _problemDetailsFactory,
            found => Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(found)));
    }

    [HttpPost]
    [SwaggerOperation("Subscribe", "Subscribe a profile to a plan.", OperationId = "Subscribe")]
    [SwaggerResponse(StatusCodes.Status201Created, "The subscription was created.", typeof(SubscriptionResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The subscription could not be created.")]
    public async Task<IActionResult> Subscribe([FromBody] SubscribeResource resource,
        CancellationToken cancellationToken)
    {
        var command = SubscribeCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await subscriptionCommandService.Handle(command, cancellationToken);

        return SubscriptionsActionResultAssembler.ToActionResultFromSubscribeResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            created => CreatedAtAction(nameof(GetSubscriptionById), new { subscriptionId = created.Id },
                SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [SwaggerOperation("Get All Subscriptions", "List all subscriptions.", OperationId = "GetAllSubscriptions")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of subscriptions.",
        typeof(IEnumerable<SubscriptionResource>))]
    public async Task<IActionResult> GetAllSubscriptions(CancellationToken cancellationToken)
    {
        var query = new GetAllSubscriptionsQuery();
        var subscriptions = await subscriptionQueryService.Handle(query, cancellationToken);
        return Ok(subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
