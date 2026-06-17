using DevilTeam.Hera.Platform.Shared.Domain.Model.Entities;

namespace DevilTeam.Hera.Platform.Iam.Domain.Model.Aggregates;

public partial class User : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}