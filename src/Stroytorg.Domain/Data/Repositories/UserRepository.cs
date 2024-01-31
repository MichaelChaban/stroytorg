using Microsoft.EntityFrameworkCore;
using Stroytorg.Domain.Data.Entities;
using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Repositories.Common;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Domain.Data.Repositories;

public class UserRepository(
    IStroytorgDbContext context,
    IUserContext httpUserContext)
    : RepositoryBase<User, int>(context, httpUserContext), IUserRepository
{
    protected override IQueryable<User> GetQueryable()
    {
        return GetDbSet().Include(x => x.Orders).AsQueryable();
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await GetDbSet().FirstOrDefaultAsync(x => x.Email.Equals(email), cancellationToken);
    }

    public async Task<bool> ExistsWithEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await GetDbSet().AnyAsync(entity => entity.Email.Equals(email), cancellationToken);
    }

    protected override DbSet<User> GetDbSet()
    {
        return StroytorgContext.User;
    }
}
