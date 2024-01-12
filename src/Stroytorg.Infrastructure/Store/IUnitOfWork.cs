namespace Stroytorg.Infrastructure.Store;

public interface IUnitOfWork
{
    Task Commit();

    void Rollback();
}