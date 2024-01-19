using Stroytorg.Application.Constants;
using Stroytorg.Application.Extensions;
using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Domain.Sorting;
using Stroytorg.Domain.Specifications;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using DB = Stroytorg.Domain;
using DbEnum = Stroytorg.Domain.Data.Enums;

namespace Stroytorg.Application.Services;

public class OrderService(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IOrderRepository orderRepository,
    IMaterialRepository materialRepository,
    IOrderServiceFacade orderServiceFacade) : IOrderService
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IOrderRepository orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    private readonly IMaterialRepository materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
    private readonly IOrderServiceFacade orderServiceFacade = orderServiceFacade ?? throw new ArgumentNullException(nameof(orderServiceFacade));

    public async Task<PagedData<Order>> GetPagedUserAsync(DataRangeRequest<OrderFilter> request)
    {
        var specification = autoMapperTypeMapper.Map<OrderSpecification>(orderServiceFacade.GetPagedUserOrdersFilter(request!.Filter));
        var filter = specification?.SatisfiedBy();

        var totalItems = await orderRepository.GetCountAsync(filter!);
        var items = await orderRepository.GetPagedSortAsync<OrderSort>(request!.Offset, request.Limit, filter!, autoMapperTypeMapper.Map<DB.Sorting.Common.SortDefinition>(request.Sort));

        var mappedItems = autoMapperTypeMapper.Map<Order>(items);
        return new PagedData<Order>(
            Data: mappedItems,
            Total: totalItems);
    }

    public async Task<PagedData<Order>> GetPagedAsync(DataRangeRequest<OrderFilter> request)
    {
        var specification = autoMapperTypeMapper.Map<OrderSpecification>(request?.Filter!);
        var filter = specification?.SatisfiedBy();

        var totalItems = await orderRepository.GetCountAsync(filter!);
        var items = await orderRepository.GetPagedSortAsync<OrderSort>(request!.Offset, request.Limit, filter!, autoMapperTypeMapper.Map<DB.Sorting.Common.SortDefinition>(request.Sort));

        var mappedItems = autoMapperTypeMapper.Map<Order>(items);
        return new PagedData<Order>(
            Data: mappedItems,
            Total: totalItems);
    }

    public async Task<BusinessResponse<OrderDetail>> GetByIdAsync(int orderId)
    {
        var order = await orderRepository.GetAsync(orderId);
        if (order is null)
        {
            return new BusinessResponse<OrderDetail>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        return new BusinessResponse<OrderDetail>(
            Value: autoMapperTypeMapper.Map<OrderDetail>(order));
    }

    public async Task<BusinessResponse<int>> CreateAsync(OrderCreate order)
    {
        var materials = await materialRepository.GetByIdsAsync(order.Materials.Select(x => x.MaterialId).ToArray());
        if (order.ValidateOrder(materials, out var businessResponse) is false)
        {
            return businessResponse!;
        }

        var orderEntity = autoMapperTypeMapper.Map<DB.Data.Entities.Order>(order);
        var orderMaterialMaps = autoMapperTypeMapper.Map<DB.Data.Entities.OrderMaterialMap>(order.Materials);

        await orderServiceFacade.CreateOrderAsync(orderEntity, materials, orderMaterialMaps);

        return new BusinessResponse<int>(
            Value: orderEntity!.Id);
    }

    public async Task<BusinessResponse<int>> UpdateAsync(int orderId, OrderEdit order)
    {
        var orderEntity = await orderRepository.GetAsync(orderId);
        if (orderEntity is null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }
        else if (orderEntity.IsActive is false)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.AlreadyInActiveEntity);
        }

        orderEntity = autoMapperTypeMapper.Map(order, orderEntity);
        if (orderEntity.OrderStatus is not (DbEnum.OrderStatus.BeingPrepared or DbEnum.OrderStatus.OutForDelivery))
        {
            await orderServiceFacade.UpdateOrderMaterialMapAsync(orderEntity);
        }

        orderRepository.Update(orderEntity);
        await orderRepository.UnitOfWork.CommitAsync();

        return new BusinessResponse<int>(
            Value: orderEntity.Id);
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
}
