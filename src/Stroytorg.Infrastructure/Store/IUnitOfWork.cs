namespace Stroytorg.Infrastructure.Store;

public interface IUnitOfWork
{
    Task Commit(CancellationToken cancellationToken = default);

    void Rollback();
}