namespace DevilTeam.Hera.Platform.Automation.Domain.Model.ValueObjects;

using Enums;

/// <summary>
/// Represents the action executed when an automation rule is triggered.
/// </summary>
public record AutomationAction
{
    public ActionType ActionType { get; init; }
    
    /// <summary>Target device id. Used when ActionType is TurnOnDevice or TurnOffDevice.</summary>
    public int? TargetDeviceId { get; init; }
    
    /// <summary>Additional value. E.g. notification message.</summary>
    public string? ActionValue { get; init; }

    private AutomationAction() { }

    public AutomationAction(ActionType actionType, int? targetDeviceId, string? actionValue)
    {
        ActionType = actionType;
        TargetDeviceId = targetDeviceId;
        ActionValue = actionValue;
    }
}