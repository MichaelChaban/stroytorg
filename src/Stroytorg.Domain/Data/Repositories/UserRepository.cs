using Microsoft.EntityFrameworkCore;
using Stroytorg.Domain.Data.Entities;
using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Repositories.Common;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Domain.Data.Repositories;

public class UserRepository : RepositoryBase<User, int>, IUserRepository
{
    public UserRepository(IStroytorgDbContext context, IUserContext httpUserContext)
            : base(context, httpUserContext)
    {
    }

    protected override IQueryable<User> GetQueryable()
    {
        return GetDbSet().Include(x => x.Orders).AsQueryable();
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
