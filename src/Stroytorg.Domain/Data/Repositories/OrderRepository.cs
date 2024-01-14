using Microsoft.EntityFrameworkCore;
using Stroytorg.Domain.Data.Entities;
using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Domain.Data.Repositories;

public class OrderRepository : RepositoryBase<Order, int>, IOrderRepository
{
    public OrderRepository(IStroytorgDbContext context, IUserContext httpUserContext)
            : base(context, httpUserContext)
    {
    }

    protected override IQueryable<Order> GetQueryable()
    {
        return GetDbSet().Include(x => x.OrderMaterialMap).AsQueryable();
    }

    protected override DbSet<Order> GetDbSet()
    {
        return StroytorgContext.Order;
    }
}