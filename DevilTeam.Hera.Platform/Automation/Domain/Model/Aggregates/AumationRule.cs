namespace DevilTeam.Hera.Platform.Automation.Domain.Model.Aggregates;

using Shared.Domain.Model.Entities;
using ValueObjects;

/// <summary>
/// Aggregate root for an automation rule in a smart home.
/// </summary>
public class AutomationRule : IAuditableEntity
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
    
    /// <summary>Home this rule belongs to.</summary>
    public int HomeId { get; private set; }
    
    /// <summary>User who created the rule.</summary>
    public int UserId { get; private set; }

    public Trigger Trigger { get; private set; }
    public AutomationAction Action { get; private set; }
    
    // IAuditableEntity
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    // Required by EF Core
    private AutomationRule() { }

    public AutomationRule(string name, string? description, int homeId, int userId,
        Trigger trigger, AutomationAction action)
    {
        Name = name;
        Description = description;
        HomeId = homeId;
        UserId = userId;
        Trigger = trigger;
        Action = action;
        IsActive = true;
    }

    /// <summary>Updates the rule's editable fields.</summary>
    public void Update(string name, string? description, Trigger trigger, AutomationAction action)
    {
        Name = name;
        Description = description;
        Trigger = trigger;
        Action = action;
    }

    /// <summary>Activates the rule.</summary>
    public void Activate() => IsActive = true;

    /// <summary>Deactivates the rule.</summary>
    public void Deactivate() => IsActive = false;
}