namespace DevilTeam.Hera.Platform.Automation.Domain.Model.ValueObjects;

using Enums;

/// <summary>
/// Represents the condition that activates an automation rule.
/// </summary>
public record Trigger
{
    public TriggerType TriggerType { get; init; }
    
    /// <summary>Time in HH:mm format. Used when TriggerType is TimeBased.</summary>
    public string? ScheduledTime { get; init; }
    
    /// <summary>Device id to watch. Used when TriggerType is DeviceState.</summary>
    public int? DeviceId { get; init; }
    
    /// <summary>Expected value to compare. E.g. "true", "500".</summary>
    public string? ConditionValue { get; init; }

    // Parameterless constructor required by EF Core
    private Trigger() { }

    public Trigger(TriggerType triggerType, string? scheduledTime, int? deviceId, string? conditionValue)
    {
        TriggerType = triggerType;
        ScheduledTime = scheduledTime;
        DeviceId = deviceId;
        ConditionValue = conditionValue;
    }
}