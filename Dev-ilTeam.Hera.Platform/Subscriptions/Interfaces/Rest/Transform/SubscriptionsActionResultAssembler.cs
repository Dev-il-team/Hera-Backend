using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model;
using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;
using Dev_ilTeam.Hera.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Dev_ilTeam.Hera.Platform.Subscriptions.Interfaces.Rest.Transform;

public static class SubscriptionsActionResultAssembler
{
    private static int ToStatusCodeFromSubscriptionsError(SubscriptionsError error)
    {
        return error switch
        {
            SubscriptionsError.SubscriptionNotFound => StatusCodes.Status404NotFound,
            SubscriptionsError.ProfileAlreadySubscribed => StatusCodes.Status409Conflict,
            SubscriptionsError.OperationCancelled => StatusCodes.Status409Conflict,
            SubscriptionsError.DatabaseError => StatusCodes.Status500InternalServerError,
            SubscriptionsError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromSubscribeResult(
        ControllerBase controller,
        Result<Subscription> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Subscription, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCodeFromSubscriptionsError((SubscriptionsError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetSubscriptionByIdResult(
        ControllerBase controller,
        Subscription? subscription,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Subscription, IActionResult> successAction)
    {
        if (subscription is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromSubscriptionsError(SubscriptionsError.SubscriptionNotFound),
                SubscriptionsError.SubscriptionNotFound,
                errorLocalizer[nameof(SubscriptionsError.SubscriptionNotFound)]);
        return successAction(subscription);
    }
}
