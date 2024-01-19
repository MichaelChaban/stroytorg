using Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Domain.Data.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Order, int>
{
    Task<IEnumerable<Order>> GetOrdersByEmailAsync(string email);
}
