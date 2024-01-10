using Microsoft.EntityFrameworkCore;
using Stroytorg.Domain.Data.Entities;
using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Domain.Data.Repositories;

public class CategoryRepository : RepositoryBase<Category, int>, ICategoryRepository
{
    public CategoryRepository(IStroytorgDbContext context, IUserContext httpUserContext)
            : base(context, httpUserContext)
    {
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await GetDbSet().FirstOrDefaultAsync(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    protected override DbSet<Category> GetDbSet()
    {
        return StroytorgContext.Category;
    }
}
