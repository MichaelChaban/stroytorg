using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Application.Features.Orders.Commands;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using DbEnum = Stroytorg.Domain.Data.Enums;

namespace Stroytorg.Application.Features.Orders.CommandHandlers;

public class UpdateOrderCommandHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IOrderRepository orderRepository,
    IOrderFacade orderFacade) :
    IRequestHandler<UpdateOrderCommand, BusinessResponse<int>>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IOrderRepository orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    private readonly IOrderFacade orderFacade = orderFacade ?? throw new ArgumentNullException(nameof(orderFacade));

    public async Task<BusinessResponse<int>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = await orderRepository.GetAsync(request.OrderId, cancellationToken);
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

        orderEntity = autoMapperTypeMapper.Map(request.Order, orderEntity);
        if (orderEntity.OrderStatus is not (DbEnum.OrderStatus.BeingPrepared or DbEnum.OrderStatus.OutForDelivery))
        {
            await orderFacade.UpdateOrderMaterialMapAsync(orderEntity);
        }

        orderRepository.Update(orderEntity);
        await orderRepository.UnitOfWork.CommitAsync();

        return new BusinessResponse<int>(
            Value: orderEntity.Id);
    }
}
