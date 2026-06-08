namespace DevilTeam.Hera.Platform.Automation.Interfaces.REST.Resources;

using Domain.Model.Enums;

/// <summary>
/// Request resource to create a new automation rule.
/// </summary>
public record CreateAutomationRuleResource(
    string Name,
    string? Description,
    int HomeId,
    int UserId,
    // Trigger
    TriggerType TriggerType,
    string? ScheduledTime,
    int? DeviceId,
    string? ConditionValue,
    // Action
    ActionType ActionType,
    int? TargetDeviceId,
    string? ActionValue
);