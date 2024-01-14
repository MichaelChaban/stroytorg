using Microsoft.EntityFrameworkCore;
using Stroytorg.Domain.Data.Entities;
using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Domain.Data.Repositories;

public class MaterialRepository : RepositoryBase<Material, int>, IMaterialRepository
{
    public MaterialRepository(IStroytorgDbContext context, IUserContext httpUserContext)
            : base(context, httpUserContext)
    {
    }

    protected override IQueryable<Material> GetQueryable()
    {
        return GetDbSet().Include(x => x.Category).AsQueryable();
    }

    public async Task<Material?> GetByNameAsync(string name)
    {
        return await GetDbSet().FirstOrDefaultAsync(x => x.Name.ToUpper().Equals(name.ToUpper()));
    }

    protected override DbSet<Material> GetDbSet()
    {
        return StroytorgContext.Material;
    }
}