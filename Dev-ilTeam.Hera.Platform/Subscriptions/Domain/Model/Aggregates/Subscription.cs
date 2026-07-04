using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.ValueObjects;

namespace Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Model.Aggregates;

public partial class Subscription
{
    public Subscription()
    {
        Plan = EPlanType.Basic;
        IsActive = true;
    }

    public Subscription(int profileId, EPlanType plan) : this()
    {
        ProfileId = profileId;
        Plan = plan;
    }

    public Subscription(SubscribeCommand command) : this(command.ProfileId, command.Plan)
    {
    }

    public int Id { get; }
    public int ProfileId { get; private set; }
    public EPlanType Plan { get; private set; }
    public bool IsActive { get; private set; }

    public Subscription UpgradeToPremium()
    {
        Plan = EPlanType.Premium;
        return this;
    }

    public Subscription DowngradeToBasic()
    {
        Plan = EPlanType.Basic;
        return this;
    }

    public void Cancel()
    {
        IsActive = false;
    }
}
