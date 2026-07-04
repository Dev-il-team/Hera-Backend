using Dev_ilTeam.Hera.Platform.Subscriptions.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model;
using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;
using Dev_ilTeam.Hera.Platform.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Dev_ilTeam.Hera.Platform.Subscriptions.Application.Internal.CommandServices;

public class SubscriptionCommandService(
    ISubscriptionRepository subscriptionRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : ISubscriptionCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;

    public async Task<Result<Subscription>> Handle(SubscribeCommand command, CancellationToken cancellationToken)
    {
        if (await subscriptionRepository.ExistsByProfileIdAsync(command.ProfileId, cancellationToken))
            return Result<Subscription>.Failure(SubscriptionsError.ProfileAlreadySubscribed,
                _localizer[nameof(SubscriptionsError.ProfileAlreadySubscribed)]);
        var subscription = new Subscription(command);
        try
        {
            await subscriptionRepository.AddAsync(subscription, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Subscription>.Success(subscription);
        }
        catch (OperationCanceledException)
        {
            return Result<Subscription>.Failure(SubscriptionsError.OperationCancelled,
                _localizer[nameof(SubscriptionsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Subscription>.Failure(SubscriptionsError.DatabaseError,
                _localizer[nameof(SubscriptionsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Subscription>.Failure(SubscriptionsError.InternalServerError,
                _localizer[nameof(SubscriptionsError.InternalServerError)]);
        }
    }
}
