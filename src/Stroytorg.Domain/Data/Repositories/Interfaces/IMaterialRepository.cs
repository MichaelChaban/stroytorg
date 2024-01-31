using Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Domain.Data.Repositories.Interfaces;

public interface IMaterialRepository : IRepository<Material, int>
{
    Task<Material?> GetByNameAsync(string name, CancellationToken cancellationToken);

    Task<bool> ExistsWithCategoryIdAsync(int categoryId, CancellationToken cancellationToken);

    Task<bool> ExistsWithNameAsync(string name, CancellationToken cancellationToken);
}
