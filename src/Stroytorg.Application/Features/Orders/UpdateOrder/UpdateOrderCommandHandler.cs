using MediatR;
using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Infrastructure.Validations.Common;
using DbEnum = Stroytorg.Domain.Data.Enums;

namespace Stroytorg.Application.Features.Orders.UpdateOrder;

public class UpdateOrderCommandHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IOrderRepository orderRepository,
    IOrderFacade orderFacade) :
    IRequestHandler<UpdateOrderCommand, BusinessResult<int>>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IOrderRepository orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    private readonly IOrderFacade orderFacade = orderFacade ?? throw new ArgumentNullException(nameof(orderFacade));

    public async Task<BusinessResult<int>> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderEntity = await orderRepository.GetAsync(command.OrderId, cancellationToken);
        orderEntity = autoMapperTypeMapper.Map(command, orderEntity);

        if (orderEntity.OrderStatus is not (DbEnum.OrderStatus.BeingPrepared or DbEnum.OrderStatus.OutForDelivery))
        {
            await orderFacade.UpdateOrderMaterialMapAsync(orderEntity);
        }

        orderRepository.Update(orderEntity);
        await orderRepository.UnitOfWork.CommitAsync(cancellationToken);

        return BusinessResult.Success(orderEntity.Id);
    }
}
