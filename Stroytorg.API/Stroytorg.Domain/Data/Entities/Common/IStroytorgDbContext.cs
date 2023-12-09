using Stroytorg.Infrastructure.Store;

namespace Stroytorg.Domain.Data.Entities.Common;

public interface IStroytorgDbContext : IUnitOfWork
{
    void Migrate();
}
