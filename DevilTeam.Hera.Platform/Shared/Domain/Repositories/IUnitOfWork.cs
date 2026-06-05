namespace DevilTeam.Hera.Platform.Shared.Domain.Repositories;

/// <summary>
///     Unit of work interface for all repositories.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Save changes to the repository.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task CompleteAsync(CancellationToken cancellationToken = default);
}