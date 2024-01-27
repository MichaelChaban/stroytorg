using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Materials.UpdateMaterial;

public class UpdateMaterialCommandHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IMaterialRepository materialRepository)
    : ICommandHandler<UpdateMaterialCommand, int>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IMaterialRepository materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));

    public async Task<BusinessResult<int>> Handle(UpdateMaterialCommand command, CancellationToken cancellationToken)
    {
        var materialEntity = await materialRepository.GetAsync(command.MaterialId, cancellationToken);
        materialEntity = autoMapperTypeMapper.Map(command, materialEntity);

        materialRepository.Update(materialEntity);
        await materialRepository.UnitOfWork.CommitAsync(cancellationToken);

        return BusinessResult.Success(materialEntity.Id);
    }
}
