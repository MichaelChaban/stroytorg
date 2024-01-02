using Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Domain.Data.Repositories.Interfaces;

public interface IUserRepository : IRepository<User, int>
{
    Task<User?> GetByIdAsync(int userId);

    Task<User?> GetByEmailAsync(string email);
}
