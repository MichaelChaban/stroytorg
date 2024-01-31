namespace Stroytorg.Infrastructure.Store;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);

    void Rollback();
}