namespace DevilTeam.Hera.Platform.Automation.Interfaces.REST.Transform;

using Application.Internal.Commands;
using Resources;

/// <summary>
/// Converts a CreateAutomationRuleResource into a CreateAutomationRuleCommand.
/// </summary>
public static class CreateAutomationRuleCommandFromResourceAssembler
{
    public static CreateAutomationRuleCommand ToCommandFromResource(
        CreateAutomationRuleResource resource)
    {
        return new CreateAutomationRuleCommand(
            resource.Name,
            resource.Description,
            resource.HomeId,
            resource.UserId,
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