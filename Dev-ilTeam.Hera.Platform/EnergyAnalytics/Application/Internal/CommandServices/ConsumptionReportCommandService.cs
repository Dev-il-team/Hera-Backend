using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;
using Dev_ilTeam.Hera.Platform.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Application.Internal.CommandServices;

public class ConsumptionReportCommandService(
    IConsumptionReportRepository consumptionReportRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IConsumptionReportCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;

    public async Task<Result<ConsumptionReport>> Handle(RecordConsumptionCommand command,
        CancellationToken cancellationToken)
    {
        var report = new ConsumptionReport(command);
        try
        {
            await consumptionReportRepository.AddAsync(report, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<ConsumptionReport>.Success(report);
        }
        catch (OperationCanceledException)
        {
            return Result<ConsumptionReport>.Failure(EnergyAnalyticsError.OperationCancelled,
                _localizer[nameof(EnergyAnalyticsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<ConsumptionReport>.Failure(EnergyAnalyticsError.DatabaseError,
                _localizer[nameof(EnergyAnalyticsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<ConsumptionReport>.Failure(EnergyAnalyticsError.InternalServerError,
                _localizer[nameof(EnergyAnalyticsError.InternalServerError)]);
        }
    }
}
