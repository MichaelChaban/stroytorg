using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Infrastructure.Validations.Common;
using DbData = Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Application.Features.Orders.CreateOrder;

public class CreateOrderCommandHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IMaterialRepository materialRepository,
    IOrderFacade orderFacade)
    : ICommandHandler<CreateOrderCommand, int>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IMaterialRepository materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
    private readonly IOrderFacade orderFacade = orderFacade ?? throw new ArgumentNullException(nameof(orderFacade));

    public async Task<BusinessResult<int>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var materials = await materialRepository.GetByIdsAsync(command.Materials.Select(x => x.MaterialId).ToArray(), cancellationToken);

        var orderEntity = autoMapperTypeMapper.Map<DbData.Order>(command);
        var orderMaterialMaps = autoMapperTypeMapper.Map<DbData.OrderMaterialMap>(command.Materials);

        await orderFacade.CreateOrderAsync(orderEntity, materials, orderMaterialMaps);

        return BusinessResult.Success(orderEntity!.Id);
    }
}
