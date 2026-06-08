namespace DevilTeam.Hera.Platform.Automation.Application.Internal.Commands;

using Domain.Model.Enums;

/// <summary>
/// Command to update an existing automation rule.
/// </summary>
public record UpdateAutomationRuleCommand(
    int Id,
    string Name,
    string? Description,
    // Trigger fields
    TriggerType TriggerType,
    string? ScheduledTime,
    int? DeviceId,
    string? ConditionValue,
    // Action fields
    ActionType ActionType,
    int? TargetDeviceId,
    string? ActionValue
);