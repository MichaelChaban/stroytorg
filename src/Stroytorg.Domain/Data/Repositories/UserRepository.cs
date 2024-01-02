using Microsoft.EntityFrameworkCore;
using Stroytorg.Domain.Data.Entities;
using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Domain.Data.Repositories;

public class UserRepository : RepositoryBase<User, int>, IUserRepository
{
    public UserRepository(IStroytorgDbContext context, IUserContext httpUserContext)
            : base(context, httpUserContext)
    {
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        return await GetDbSet().Include(x => x.Orders).FirstOrDefaultAsync(x => x.Id.Equals(userId));
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await GetDbSet().FirstOrDefaultAsync(x => x.Email.Equals(email));
    }

    protected override DbSet<User> GetDbSet()
    {
        return StroytorgContext.User;
    }
}
