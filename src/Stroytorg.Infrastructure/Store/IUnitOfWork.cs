namespace Stroytorg.Infrastructure.Store;

public interface IUnitOfWork
{
    Task CommitAsync();

    void Rollback();
}