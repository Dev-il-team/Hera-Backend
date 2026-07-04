using Dev_ilTeam.Hera.Platform.Shared.Domain.Model.Entities;

namespace Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Aggregates;

public partial class Subscription : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
