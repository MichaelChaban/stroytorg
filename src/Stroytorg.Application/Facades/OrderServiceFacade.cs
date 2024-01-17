using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using DbEntity = Stroytorg.Domain.Data.Entities;
using DbEnum = Stroytorg.Domain.Data.Enums;

namespace Stroytorg.Application.Facades;

public class OrderServiceFacade : IOrderServiceFacade
{
    private readonly IMaterialRepository materialRepository;
    private readonly IOrderRepository orderRepository;
    private readonly IUserRepository userRepository;
    private readonly IOrderMaterialMapRepository orderMaterialMapRepository;
    private readonly IUserContext userContext;

    private const int MaterialsToOrder = -1;
    private const int MaterialsFromOrder = 1;

    public OrderServiceFacade(
        IMaterialRepository materialRepository,
        IOrderRepository orderRepository,
        IUserRepository userRepository,
        IOrderMaterialMapRepository orderMaterialMapRepository,
        IUserContext userContext)
    {
        this.materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
        this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        this.orderMaterialMapRepository = orderMaterialMapRepository ?? throw new ArgumentNullException(nameof(orderMaterialMapRepository));
        this.userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
    }

    public async Task CreateOrderAsync(DbEntity.Order order, IEnumerable<DbEntity.Material> materials, IEnumerable<DbEntity.OrderMaterialMap> orderMaterialMaps)
    {
        order.UserId = await GetExisingUserIdAsync(order.Email);

        await UpdateMaterialsAsync(materials, order);
        await AddOrderAsync(order);
        await CreateOrderMaterialMapAsync(orderMaterialMaps, materials, order.Id);
    }


    public async Task UpdateOrderMaterialMapAsync(DbEntity.Order order)
    {
        if (order.OrderStatus is DbEnum.OrderStatus.Completed)
        {
            orderMaterialMapRepository.DeactivateRange(order.OrderMaterialMap.ToArray());
        }
        else if (order.OrderStatus is DbEnum.OrderStatus.Cancelled)
        {
            orderMaterialMapRepository.DeactivateRange(order.OrderMaterialMap.ToArray());
            var materials = order.OrderMaterialMap.Select(x => x.Material).ToList();
            await UpdateMaterialsAsync(materials!, order);
        }
        await orderMaterialMapRepository.UnitOfWork.CommitAsync();
    }
    public string GetCurrentUserEmail()
    {
        return userContext.User.Identity?.Name ?? "uknown@example.com";
    }

    private async Task AddOrderAsync(DbEntity.Order order)
    {
        await orderRepository.AddAsync(order);
        await orderRepository.UnitOfWork.CommitAsync();
    }

    private async Task UpdateMaterialsAsync(IEnumerable<DbEntity.Material> materials, DbEntity.Order order)
    {
        var coefficient = MaterialsToOrder;
        switch (order.OrderStatus)
        {
            case DbEnum.OrderStatus.Cancelled: coefficient = MaterialsFromOrder; break;
            default: coefficient = 0; break;
        }
        foreach (var material in materials)
        {
            material.StockAmount += order.OrderMaterialMap.FirstOrDefault(x => x.Id == material.Id)!.TotalMaterialAmount * coefficient;
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
            orderMaterialMap.TotalMaterialWeight = material.Weight.HasValue ? material.Weight.Value * orderMaterialMap.TotalMaterialAmount : null;
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
