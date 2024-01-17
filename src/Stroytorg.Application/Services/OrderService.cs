using Stroytorg.Application.Constants;
using Stroytorg.Application.Extensions;
using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Domain.Sorting;
using Stroytorg.Domain.Specifications;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using DB = Stroytorg.Domain;

namespace Stroytorg.Application.Services;

public class OrderService : IOrderService
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper;
    private readonly IOrderRepository orderRepository;
    private readonly IMaterialRepository materialRepository;
    private readonly IOrderServiceFacade orderServiceFacade;

    public OrderService(
        IAutoMapperTypeMapper autoMapperTypeMapper,
        IOrderRepository orderRepository,
        IMaterialRepository materialRepository,
        IOrderServiceFacade orderServiceFacade)
    {
        this.autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
        this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        this.materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
        this.orderServiceFacade = orderServiceFacade ?? throw new ArgumentNullException(nameof(orderServiceFacade));
    }

    public async Task<PagedData<Order>> GetPagedForUserAsync(DataRangeRequest<OrderFilter> request)
    {
        var currentUserEmail = orderServiceFacade.GetCurrentUserEmail();
        var specification = autoMapperTypeMapper.Map<OrderSpecification>(request?.Filter! with { Email = currentUserEmail });
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

    public async Task<BusinessResponse<Order>> GetByIdAsync(int orderId)
    {
        var order = await orderRepository.GetAsync(orderId);
        if (order is null)
        {
            return new BusinessResponse<Order>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        return new BusinessResponse<Order>(
            Value: autoMapperTypeMapper.Map<Order>(order));
    }

    public async Task<BusinessResponse<int>> CreateAsync(OrderCreate order)
    {
        var materials = await materialRepository.GetByIdsAsync(order.MaterialMap.Select(x => x.MaterialId).ToArray());
        if (!order.ValidateOrder(materials, out var businessResponse))
        {
            return businessResponse!;
        }

        var orderEntity = autoMapperTypeMapper.Map<DB.Data.Entities.Order>(order);
        var orderMaterialMaps = autoMapperTypeMapper.Map<DB.Data.Entities.OrderMaterialMap>(order.MaterialMap);

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

        orderEntity = autoMapperTypeMapper.Map(order, orderEntity);
        await orderServiceFacade.UpdateOrderMaterialMapAsync(orderEntity);

        orderRepository.Update(orderEntity);
        await orderRepository.UnitOfWork.CommitAsync();

        return new BusinessResponse<int>(
            Value: orderEntity.Id);
    }
}
