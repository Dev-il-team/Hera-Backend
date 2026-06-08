namespace DevilTeam.Hera.Platform.Automation.Interfaces.REST.Transform;

using Application.Internal.Commands;
using Resources;

/// <summary>
/// Converts an UpdateAutomationRuleResource into an UpdateAutomationRuleCommand.
/// </summary>
public static class UpdateAutomationRuleCommandFromResourceAssembler
{
    public static UpdateAutomationRuleCommand ToCommandFromResource(
        int id, UpdateAutomationRuleResource resource)
    {
        return new UpdateAutomationRuleCommand(
            id,
            resource.Name,
            resource.Description,
            resource.TriggerType,
            resource.ScheduledTime,
            resource.DeviceId,
            resource.ConditionValue,
            resource.ActionType,
            resource.TargetDeviceId,
            resource.ActionValue
        );
    }
}