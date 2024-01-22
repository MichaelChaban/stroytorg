using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using DbEntity = Stroytorg.Domain.Data.Entities;
using DbEnum = Stroytorg.Domain.Data.Enums;

namespace Stroytorg.Application.Facades;

public class OrderFacade(
    IMaterialRepository materialRepository,
    IOrderRepository orderRepository,
    IUserRepository userRepository,
    IOrderMaterialMapRepository orderMaterialMapRepository,
    IUserContext userContext) : IOrderFacade
{
    private readonly IMaterialRepository materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
    private readonly IOrderRepository orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    private readonly IUserRepository userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IOrderMaterialMapRepository orderMaterialMapRepository = orderMaterialMapRepository ?? throw new ArgumentNullException(nameof(orderMaterialMapRepository));
    private readonly IUserContext userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));

    private const int MaterialsToOrder = -1;
    private const int MaterialsFromOrder = 1;

    public async Task CreateOrderAsync(DbEntity.Order order, IEnumerable<DbEntity.Material> materials, IEnumerable<DbEntity.OrderMaterialMap> orderMaterialMaps)
    {
        order.UserId = await GetExisingUserIdAsync(order.Email);

        await AddOrderAsync(order);
        await UpdateMaterialsAsync(materials, order, orderMaterialMaps);
        await CreateOrderMaterialMapAsync(orderMaterialMaps, materials, order.Id);
    }

    public OrderFilter GetPagedUserOrdersFilter(OrderFilter? filter)
    {
        return filter is null
            ? new OrderFilter(null, GetCurrentUserEmail(), null, null, null, null, null)
            : filter with { Email = GetCurrentUserEmail() };
    }

    public async Task UpdateOrderMaterialMapAsync(DbEntity.Order order)
    {
        if (order.OrderStatus is DbEnum.OrderStatus.Completed)
        {
            orderMaterialMapRepository.DeactivateRange(order.OrderMaterialMap!.ToArray());
        }
        else if (order.OrderStatus is DbEnum.OrderStatus.Cancelled)
        {
            orderMaterialMapRepository.DeactivateRange(order.OrderMaterialMap!.ToArray());
            var materials = order.OrderMaterialMap!.Select(x => x.Material).ToList();
            await UpdateMaterialsAsync(materials!, order, order.OrderMaterialMap!);
        }
        await orderMaterialMapRepository.UnitOfWork.CommitAsync();
    }
    public string GetCurrentUserEmail()
    {
        return userContext.User.Identity?.Name ?? "uknown@example.com";
    }

    public async Task AssignOrderToUserAsync(User user)
    {
        var orders = await orderRepository.GetOrdersByEmailAsync(user.Email);
        foreach (var order in orders)
        {
            order.UserId = user.Id;
        }
        orderRepository.UpdateRange(orders);
        await orderRepository.UnitOfWork.CommitAsync();
    }

    private async Task AddOrderAsync(DbEntity.Order order)
    {
        await orderRepository.AddAsync(order);
        await orderRepository.UnitOfWork.CommitAsync();
    }

    private async Task UpdateMaterialsAsync(IEnumerable<DbEntity.Material> materials, DbEntity.Order order, IEnumerable<DbEntity.OrderMaterialMap> orderMaterialMaps)
    {
        var coefficient = 0;
        switch (order.OrderStatus)
        {
            case DbEnum.OrderStatus.NewOrder: coefficient = MaterialsToOrder; break;
            case DbEnum.OrderStatus.Cancelled: coefficient = MaterialsFromOrder; break;
            default: break;
        }
        foreach (var material in materials)
        {
            material.StockAmount += orderMaterialMaps.FirstOrDefault(x => x.MaterialId == material.Id)!.TotalMaterialAmount * coefficient;
        }

        materialRepository.UpdateRange(materials);
        await materialRepository.UnitOfWork.CommitAsync();
    }

    private async Task CreateOrderMaterialMapAsync(IEnumerable<DbEntity.OrderMaterialMap> orderMaterialMaps, IEnumerable<DbEntity.Material> materials, int orderId)
    {
        foreach (var orderMaterialMap in orderMaterialMaps)
        {
            var material = materials.FirstOrDefault(x => x.Id == orderMaterialMap.MaterialId)!;
            orderMaterialMap.UnitPrice = material.Price;
            orderMaterialMap.OrderId = orderId;
        }

        orderMaterialMapRepository.UpdateRange(orderMaterialMaps);
        await orderMaterialMapRepository.UnitOfWork.CommitAsync();
    }

    private async Task<int?> GetExisingUserIdAsync(string email)
    {
        var user = await userRepository.GetByEmailAsync(userContext.User.Identity?.Name ?? email);
        return user?.Id;
    }
}
