using Dev_ilTeam.Hera.Platform.Shared.Domain.Model.Entities;

namespace Dev_ilTeam.Hera.Platform.Iam.Domain.Model.Aggregates;

public partial class User : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
