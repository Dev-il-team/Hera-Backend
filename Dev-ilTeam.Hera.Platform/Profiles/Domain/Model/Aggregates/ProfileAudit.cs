using Dev_ilTeam.Hera.Platform.Shared.Domain.Model.Entities;

namespace Dev_ilTeam.Hera.Platform.Profiles.Domain.Model.Aggregates;

public partial class Profile : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
