using Microsoft.EntityFrameworkCore;
using Stroytorg.Domain.Data.Entities;
using Stroytorg.Domain.Data.Repositories.Common;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.Store;

namespace Stroytorg.Domain.Data.Repositories;

public class OrderMaterialMapRepository : RepositoryBase<OrderMaterialMap, int>, IOrderMaterialMapRepository
{
    public OrderMaterialMapRepository(IUnitOfWork unitOfWork, IUserContext httpUserContext)
        : base(unitOfWork, httpUserContext)
    {
    }

    protected override IQueryable<OrderMaterialMap> GetQueryable()
    {
        return GetDbSet()
                .Include(x => x.Order)
                .Include(x => x.Material)
                .AsQueryable();
    }

    protected override DbSet<OrderMaterialMap> GetDbSet()
    {
        return StroytorgContext.OrderMaterialMap;
    }
}
