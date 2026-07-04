using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Queries;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Repositories;

namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Application.Internal.QueryServices;

public class ConsumptionReportQueryService(IConsumptionReportRepository consumptionReportRepository)
    : IConsumptionReportQueryService
{
    public async Task<IEnumerable<ConsumptionReport>> Handle(GetAllConsumptionReportsQuery query,
        CancellationToken cancellationToken)
    {
        return await consumptionReportRepository.ListAsync(cancellationToken);
    }

    public async Task<ConsumptionReport?> Handle(GetConsumptionReportByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await consumptionReportRepository.FindByIdAsync(query.ConsumptionReportId, cancellationToken);
    }
}