using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Interfaces.Rest.Resources;

namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Interfaces.Rest.Transform;

public static class ConsumptionReportResourceFromEntityAssembler
{
    public static ConsumptionReportResource ToResourceFromEntity(ConsumptionReport entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "ConsumptionReport entity cannot be null when converting to resource.");
        return new ConsumptionReportResource(entity.Id, entity.DeviceName, entity.Period, entity.KilowattHours,
            entity.Cost);
    }
}