using Dev_ilTeam.Hera.Platform.Shared.Domain.Model.Entities;

namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Aggregates;

public partial class ConsumptionReport : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}