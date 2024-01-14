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

    public OrderService(
        IAutoMapperTypeMapper autoMapperTypeMapper,
        IMaterialRepository materialRepository,
        IOrderRepository orderRepository,
        IUserRepository userRepository)
    {
        this.autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
        this.materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
        this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
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
        var materials = await materialRepository.GetByIdsAsync(order.MaterialIds.ToArray());
        if (materials.Count() != order.MaterialIds.Count)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.AlreadyExistingEntity);
        }

        order = autoMapperTypeMapper.Map(order, materialEntity);

        await materialRepository.AddAsync(materialEntity!);
        await materialRepository.UnitOfWork.Commit();

        return new BusinessResponse<int>(
            Value: materialEntity!.Id);
    }

    public async Task<BusinessResponse<int>> UpdateAsync(int materialId, MaterialEdit material)
    {
        var materialEntity = await materialRepository.GetAsync(materialId);
        if (materialEntity is null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        var category = await categoryRepository.GetAsync(material.CategoryId);
        if (category is null)
        {
            return new BusinessResponse<int>(
               IsSuccess: false,
               BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        materialEntity = autoMapperTypeMapper.Map(material, materialEntity);

        materialRepository.Update(materialEntity);
        await materialRepository.UnitOfWork.Commit();

        return new BusinessResponse<int>(
            Value: materialEntity.Id);
    }

    public async Task<BusinessResponse<int>> RemoveAsync(int materialId)
    {
        var materialEntity = await materialRepository.GetAsync(materialId);
        if (materialEntity is null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        materialRepository.Deactivate(materialEntity);
        await materialRepository.UnitOfWork.Commit();

        return new BusinessResponse<int>(
            Value: materialEntity.Id);
    }
}
