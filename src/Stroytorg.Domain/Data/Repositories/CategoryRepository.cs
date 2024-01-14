using Microsoft.EntityFrameworkCore;
using Stroytorg.Domain.Data.Entities;
using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Repositories.Common;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Domain.Data.Repositories;

public class CategoryRepository : RepositoryBase<Category, int>, ICategoryRepository
{
    public CategoryRepository(IStroytorgDbContext context, IUserContext httpUserContext)
            : base(context, httpUserContext)
    {
    }

    protected override IQueryable<Category> GetQueryable()
    {
        return GetDbSet().Include(x => x.Materials).AsQueryable();
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await GetDbSet().FirstOrDefaultAsync(x => x.Name.ToUpper().Equals(name.ToUpper()));
    }

    protected override DbSet<Category> GetDbSet()
    {
        return StroytorgContext.Category;
    }
}
