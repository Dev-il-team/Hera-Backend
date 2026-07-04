using Dev_ilTeam.Hera.Platform.Shared.Domain.Model.Entities;

namespace Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Aggregates;

public partial class Device : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
