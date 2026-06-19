using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;
using DevilTeam.Hera.Platform.Shared.Domain.Repositories;

namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Repositories;

/// <summary>
///     Device repository. CRUD comes from <see cref="IBaseRepository{TEntity}" />; the context
///     needs no extra query shapes today.
/// </summary>
public interface IDeviceRepository : IBaseRepository<Device>;
