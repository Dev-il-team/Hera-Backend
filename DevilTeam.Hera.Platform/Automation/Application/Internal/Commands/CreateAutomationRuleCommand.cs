namespace DevilTeam.Hera.Platform.Automation.Application.Internal.Commands;

using Domain.Model.Enums;

/// <summary>
/// Command to create a new automation rule.
/// </summary>
public record CreateAutomationRuleCommand(
    string Name,
    string? Description,
    int HomeId,
    int UserId,
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