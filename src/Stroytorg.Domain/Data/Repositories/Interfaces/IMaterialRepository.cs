using Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Domain.Data.Repositories.Interfaces;

public interface IMaterialRepository : IRepository<Material, int>
{
    Task<Material?> GetByNameAsync(string name);
}
