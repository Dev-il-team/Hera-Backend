using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Repositories;
using DevilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using DevilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace DevilTeam.Hera.Platform.DevicesManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Device repository implementation.
/// </summary>
public class DeviceRepository(AppDbContext context)
    : BaseRepository<Device>(context), IDeviceRepository
{
}