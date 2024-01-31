using Microsoft.EntityFrameworkCore;
using Stroytorg.Domain.Data.Entities;
using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Repositories.Common;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Domain.Data.Repositories;

public class OrderRepository : RepositoryBase<Order, int>, IOrderRepository
{
    public OrderRepository(IStroytorgDbContext context, IUserContext httpUserContext)
            : base(context, httpUserContext)
    {
    }

    public override void Deactivate(Order entity)
    {
        entity.IsActive = false;
        var fullName = HttpUserContext.User.Identity?.Name;
        entity.DeactivatedAt = DateTimeOffset.UtcNow;
        entity.DeactivatedBy = !string.IsNullOrEmpty(fullName) ? fullName : "System";
        entity.OrderStatus = Enums.OrderStatus.Cancelled;

        Update(entity);
    }

    protected override IQueryable<Order> GetQueryable()
    {
        return GetDbSet()
                .Include(x => x.OrderMaterialMap!)
                    .ThenInclude(x => x.Material)
                .Include(x => x.OrderMaterialMap)
                .AsQueryable();
    }

    protected override DbSet<Order> GetDbSet()
    {
        return StroytorgContext.Order;
    }

    public async Task<IEnumerable<Order>> GetOrdersByEmailAsync(string email)
    {
        return await GetDbSet().Where(x => x.Email == email).ToListAsync();
    }
}