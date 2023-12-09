using Stroytorg.Infrastructure.Store;

namespace Stroytorg.Domain.Data.Entities;

public interface IStroytorgDbContext : IUnitOfWork
{
    void Migrate();
}
