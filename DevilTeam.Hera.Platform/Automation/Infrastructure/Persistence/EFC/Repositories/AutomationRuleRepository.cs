namespace DevilTeam.Hera.Platform.Automation.Infrastructure.Persistence.EFC.Repositories;

using Domain.Model.Aggregates;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
/// EF Core implementation of IAutomationRuleRepository.
/// </summary>
public class AutomationRuleRepository(AppDbContext context)
    : BaseRepository<AutomationRule>(context), IAutomationRuleRepository
{
    public async Task<IEnumerable<AutomationRule>> FindByUserIdAsync(int userId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<AutomationRule>()
            .Where(r => r.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<AutomationRule>> FindByHomeIdAsync(int homeId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<AutomationRule>()
            .Where(r => r.HomeId == homeId)
            .ToListAsync(cancellationToken);
    }
}