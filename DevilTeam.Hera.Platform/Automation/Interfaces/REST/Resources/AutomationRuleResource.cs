namespace DevilTeam.Hera.Platform.Automation.Interfaces.REST.Resources;

using Domain.Model.Enums;

/// <summary>
/// Response resource for AutomationRule.
/// </summary>
public record AutomationRuleResource(
    int Id,
    string Name,
    string? Description,
    bool IsActive,
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
    string? ActionValue,
    // Audit
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt
);