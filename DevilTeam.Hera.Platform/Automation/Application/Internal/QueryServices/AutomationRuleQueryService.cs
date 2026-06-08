namespace DevilTeam.Hera.Platform.Automation.Application.Internal.QueryServices;

using Domain.Model.Aggregates;
using Domain.Repositories;
using Queries;

/// <summary>
/// Handles all read operations for AutomationRule.
/// </summary>
public class AutomationRuleQueryService(IAutomationRuleRepository automationRuleRepository)
{
    /// <summary>Returns a single rule by id.</summary>
    public async Task<AutomationRule?> Handle(GetAutomationRuleByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await automationRuleRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    /// <summary>Returns all rules for a given user.</summary>
    public async Task<IEnumerable<AutomationRule>> Handle(GetAutomationRulesByUserIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await automationRuleRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }
}