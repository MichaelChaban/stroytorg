using Stroytorg.Contracts.Filters;
using DB = Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Application.Facades.Interfaces;

public interface IOrderServiceFacade
{
    Task CreateOrderAsync(DB.Order order, IEnumerable<DB.Material> materials, IEnumerable<DB.OrderMaterialMap> orderMaterialMaps);

    Task UpdateOrderMaterialMapAsync(DB.Order order);

    string GetCurrentUserEmail();

    OrderFilter GetPagedUserOrdersFilter(OrderFilter? filter);
}
