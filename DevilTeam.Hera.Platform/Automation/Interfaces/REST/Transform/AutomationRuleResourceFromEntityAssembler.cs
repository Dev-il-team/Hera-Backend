namespace DevilTeam.Hera.Platform.Automation.Interfaces.REST.Transform;

using Domain.Model.Aggregates;
using Resources;

/// <summary>
/// Converts an AutomationRule entity into an AutomationRuleResource.
/// </summary>
public static class AutomationRuleResourceFromEntityAssembler
{
    public static AutomationRuleResource ToResourceFromEntity(AutomationRule rule)
    {
        return new AutomationRuleResource(
            rule.Id,
            rule.Name,
            rule.Description,
            rule.IsActive,
            rule.HomeId,
            rule.UserId,
            rule.Trigger.TriggerType,
            rule.Trigger.ScheduledTime,
            rule.Trigger.DeviceId,
            rule.Trigger.ConditionValue,
            rule.Action.ActionType,
            rule.Action.TargetDeviceId,
            rule.Action.ActionValue,
            rule.CreatedAt,
            rule.UpdatedAt
        );
    }
}