using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Features.Orders.Queries;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Features.Orders.QueryHandlers;

public class GetOrderQueryHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IOrderRepository orderRepository) :
    IRequestHandler<GetOrderQuery, BusinessResponse<OrderDetail>>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IOrderRepository orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));

    public async Task<BusinessResponse<OrderDetail>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            return new BusinessResponse<OrderDetail>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        return new BusinessResponse<OrderDetail>(
            Value: autoMapperTypeMapper.Map<OrderDetail>(order));
    }
}