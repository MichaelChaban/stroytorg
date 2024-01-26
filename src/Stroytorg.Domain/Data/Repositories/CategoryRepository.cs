using Microsoft.EntityFrameworkCore;
using Stroytorg.Domain.Data.Entities;
using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Repositories.Common;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Domain.Data.Repositories;

public class CategoryRepository(
    IStroytorgDbContext context,
    IUserContext httpUserContext)
    : RepositoryBase<Category, int>(context, httpUserContext), ICategoryRepository
{
    public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await GetDbSet().FirstOrDefaultAsync(category => category.Name.ToLower().Equals(name.ToLower()), cancellationToken);
    }
    public async Task<bool> ExistsWithNameAsync(string name, CancellationToken cancellationToken)
    {
        return await GetDbSet().AnyAsync(category => category.Name.ToLower().Equals(name.ToLower()), cancellationToken);
    }

    protected override IQueryable<Category> GetQueryable()
    {
        return GetDbSet()
                .Include(category => category.Materials)
                .AsQueryable();
    }

    protected override DbSet<Category> GetDbSet()
    {
        return StroytorgContext.Category;
    }
}
