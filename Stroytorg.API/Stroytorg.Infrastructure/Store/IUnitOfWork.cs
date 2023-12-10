namespace Stroytorg.Infrastructure.Store;

public interface IUnitOfWork
{
    void Commit();

    void Rollback();
}