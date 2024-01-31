using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.User;
using DB = Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Application.Facades.Interfaces;

public interface IOrderFacade
{
    Task CreateOrderAsync(DB.Order order, IEnumerable<DB.Material> materials, IEnumerable<DB.OrderMaterialMap> orderMaterialMaps);

    Task UpdateOrderMaterialMapAsync(DB.Order order);

    string GetCurrentUserEmail();

    OrderFilter GetPagedUserOrdersFilter(OrderFilter? filter);

    Task AssignOrderToUserAsync(User user);
}
