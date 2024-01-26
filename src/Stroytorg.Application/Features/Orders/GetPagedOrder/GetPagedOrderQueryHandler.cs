using MediatR;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Domain.Sorting;
using Stroytorg.Domain.Specifications;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using DbSort = Stroytorg.Domain.Sorting;

namespace Stroytorg.Application.Features.Orders.GetPagedOrder;

public class GetPagedOrderQueryHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IOrderRepository orderRepository) :
    IRequestHandler<GetPagedOrderQuery<OrderFilter>, PagedData<Order>>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IOrderRepository orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));

    public async Task<PagedData<Order>> Handle(GetPagedOrderQuery<OrderFilter> request, CancellationToken cancellationToken)
    {
        var specification = autoMapperTypeMapper.Map<OrderSpecification>(request?.Filter!);
        var filter = specification?.SatisfiedBy();

        var totalItems = await orderRepository.GetCountAsync(filter!, cancellationToken);
        var items = await orderRepository.GetPagedSortAsync<OrderSort>(request!.Offset, request.Limit, filter!, autoMapperTypeMapper.Map<DbSort.Common.SortDefinition>(request.Sort), cancellationToken);

        var mappedItems = autoMapperTypeMapper.Map<Order>(items);
        return new PagedData<Order>(
            Data: mappedItems,
            Total: totalItems);
    }
}