using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ConsumptionReportRepository(AppDbContext context)
    : BaseRepository<ConsumptionReport>(context), IConsumptionReportRepository
{
}