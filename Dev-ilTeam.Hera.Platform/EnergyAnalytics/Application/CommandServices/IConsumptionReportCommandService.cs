using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Shared.Application.Model;

namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Application.CommandServices;

public interface IConsumptionReportCommandService
{
    Task<Result<ConsumptionReport>> Handle(RecordConsumptionCommand command, CancellationToken cancellationToken);
}