using MediatR;
using Stroytorg.Application.Extensions;
using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Application.Features.Orders.Commands;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using DbData = Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Application.Features.Orders.CommandHandlers;

public class CreateOrderCommandHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IMaterialRepository materialRepository,
    IOrderFacade orderFacade) :
    IRequestHandler<CreateOrderCommand, BusinessResponse<int>>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IMaterialRepository materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
    private readonly IOrderFacade orderFacade = orderFacade ?? throw new ArgumentNullException(nameof(orderFacade));

    public async Task<BusinessResponse<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var materials = await materialRepository.GetByIdsAsync(request.Order.Materials.Select(x => x.MaterialId).ToArray(), cancellationToken);
        if (request.Order.ValidateOrder(materials, out var businessResponse) is false)
        {
            return businessResponse!;
        }

        var orderEntity = autoMapperTypeMapper.Map<DbData.Order>(request.Order);
        var orderMaterialMaps = autoMapperTypeMapper.Map<DbData.OrderMaterialMap>(request.Order.Materials);

        await orderFacade.CreateOrderAsync(orderEntity, materials, orderMaterialMaps);

        return new BusinessResponse<int>(
            Value: orderEntity!.Id);
    }
}
