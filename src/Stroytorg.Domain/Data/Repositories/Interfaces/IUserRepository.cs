using Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Domain.Data.Repositories.Interfaces;

public interface IUserRepository : IRepository<User, int>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<bool> ExistsWithEmailAsync(string email, CancellationToken cancellationToken = default);
}
