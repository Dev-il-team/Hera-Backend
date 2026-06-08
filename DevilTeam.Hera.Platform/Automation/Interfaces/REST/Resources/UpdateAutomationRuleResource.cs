namespace DevilTeam.Hera.Platform.Automation.Interfaces.REST.Resources;

using Domain.Model.Enums;

/// <summary>
/// Request resource to update an existing automation rule.
/// </summary>
public record UpdateAutomationRuleResource(
    string Name,
    string? Description,
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