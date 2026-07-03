namespace Dev_ilTeam.Hera.Platform.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync(CancellationToken cancellationToken = default);
}
