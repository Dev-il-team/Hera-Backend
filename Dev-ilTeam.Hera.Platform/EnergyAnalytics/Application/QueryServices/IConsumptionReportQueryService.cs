using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Queries;

namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Application.QueryServices;

public interface IConsumptionReportQueryService
{
    Task<IEnumerable<ConsumptionReport>> Handle(GetAllConsumptionReportsQuery query, CancellationToken cancellationToken);
    Task<ConsumptionReport?> Handle(GetConsumptionReportByIdQuery query, CancellationToken cancellationToken);
}