using Stroytorg.Application.Constants;
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
    private readonly IMaterialRepository materialRepository;
    private readonly IOrderRepository orderRepository;
    private readonly IUserRepository userRepository;

    public OrderService(
        IAutoMapperTypeMapper autoMapperTypeMapper,
        IMaterialRepository materialRepository,
        IOrderRepository orderRepository,
        IUserRepository userRepository)
    {
        this.autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
        this.materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
        this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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
        // TODO: Validation of materials properties
        if (materials is null || materials.Count() != order.MaterialMap.Count)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntities);
        }

        var orderEntity = autoMapperTypeMapper.Map<DB.Data.Entities.Order>(order);

        // TODO: implement adding orders to a new user 
        var user = await userRepository.GetByEmailAsync(order.Email);
        if (user is not null)
        {
            orderEntity.UserId = user.Id;
        }

        foreach (var material in materials)
        {
            material.StockAmount -= orderEntity.OrderMaterialMap.FirstOrDefault(x => x.Id == material.Id)!.TotalMaterialAmount;
        }

        await orderRepository.AddAsync(orderEntity);
        await orderRepository.UnitOfWork.CommitAsync();

        materialRepository.UpdateRange(materials);
        await materialRepository.UnitOfWork.CommitAsync();

        return new BusinessResponse<int>(
            Value: orderEntity!.Id);
    }

    public async Task<BusinessResponse<int>> UpdateAsync(int orderId, OrderEdit order)
    {
        // Adding validation for updating the order
        var orderEntity = await orderRepository.GetAsync(orderId);
        if (orderEntity is null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        orderEntity = autoMapperTypeMapper.Map(order, orderEntity);

        orderRepository.Update(orderEntity);
        await orderRepository.UnitOfWork.CommitAsync();

        return new BusinessResponse<int>(
            Value: orderEntity.Id);
    }

    public async Task<BusinessResponse<int>> RemoveAsync(int orderId)
    {
        var orderEntity = await orderRepository.GetAsync(orderId);
        if (orderEntity is null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        orderRepository.Deactivate(orderEntity);
        await materialRepository.UnitOfWork.CommitAsync();

        return new BusinessResponse<int>(
            Value: orderEntity.Id);
    }
}
