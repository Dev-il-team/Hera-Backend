using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dev_ilTeam.Hera.Platform.Devices.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class DeviceRepository(AppDbContext context) : BaseRepository<Device>(context), IDeviceRepository
{
    public async Task<IEnumerable<Device>> FindByRoomIdAsync(int roomId, CancellationToken cancellationToken)
    {
        return await Context.Set<Device>()
            .Include(device => device.Room)
            .Where(device => device.RoomId == roomId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await Context.Set<Device>().AnyAsync(device => device.Name == name, cancellationToken);
    }

    public new async Task<Device?> FindByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<Device>()
            .Include(device => device.Room)
            .FirstOrDefaultAsync(device => device.Id == id, cancellationToken);
    }

    public new async Task<IEnumerable<Device>> ListAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<Device>()
            .Include(device => device.Room)
            .ToListAsync(cancellationToken);
    }
}
