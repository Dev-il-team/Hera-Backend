using Dev_ilTeam.Hera.Platform.Shared.Domain.Model;

namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Errors;

public static class EnergyAnalyticsErrors
{
    public static readonly Error ConsumptionReportNotFound =
        new("EnergyAnalytics.ConsumptionReportNotFound", "The specified consumption report was not found.");
}