namespace DevilTeam.Hera.Platform.Automation.Domain.Repositories;

using Model.Aggregates;
using Shared.Domain.Repositories;

/// <summary>
/// Repository contract for AutomationRule aggregate.
/// </summary>
public interface IAutomationRuleRepository : IBaseRepository<AutomationRule>
{
    /// <summary>Returns all rules belonging to a specific user.</summary>
    Task<IEnumerable<AutomationRule>> FindByUserIdAsync(int userId, 
        CancellationToken cancellationToken = default);

    /// <summary>Returns all rules belonging to a specific home.</summary>
    Task<IEnumerable<AutomationRule>> FindByHomeIdAsync(int homeId, 
        CancellationToken cancellationToken = default);
}