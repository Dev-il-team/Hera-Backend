using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;
using DevilTeam.Hera.Platform.Shared.Domain.Repositories;

namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Repositories;

/// <summary>
///     Device repository interface.
/// </summary>
public interface IDeviceRepository : IBaseRepository<Device>
{
}