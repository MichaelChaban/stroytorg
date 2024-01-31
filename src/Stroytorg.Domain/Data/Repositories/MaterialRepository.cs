using Microsoft.EntityFrameworkCore;
using Stroytorg.Domain.Data.Entities;
using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Repositories.Common;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Domain.Data.Repositories;

public class MaterialRepository(
    IStroytorgDbContext context,
    IUserContext httpUserContext)
    : RepositoryBase<Material, int>(context, httpUserContext), IMaterialRepository
{
    public async Task<bool> ExistsWithCategoryIdAsync(int categoryId, CancellationToken cancellationToken)
    {
        return await GetDbSet().AnyAsync(x => x.CategoryId == categoryId, cancellationToken);
    }

    public async Task<bool> ExistsWithNameAsync(string name, CancellationToken cancellationToken)
    {
        return await GetDbSet().AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()), cancellationToken);
    }


    public async Task<Material?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await GetDbSet().FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()), cancellationToken);
    }

    protected override IQueryable<Material> GetQueryable()
    {
        return GetDbSet().Include(x => x.Category).AsQueryable();
    }

    protected override DbSet<Material> GetDbSet()
    {
        return StroytorgContext.Material;
    }
}