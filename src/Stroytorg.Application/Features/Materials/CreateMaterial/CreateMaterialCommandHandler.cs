using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Materials.CreateMaterial;

public class CreateMaterialCommandHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IMaterialRepository materialRepository)
    : ICommandHandler<CreateMaterialCommand, int>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IMaterialRepository materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));

    public async Task<BusinessResult<int>> Handle(CreateMaterialCommand command, CancellationToken cancellationToken)
    {
        var materialEntity = await materialRepository.GetByNameAsync(command.Name, cancellationToken);
        materialEntity = autoMapperTypeMapper.Map(command, materialEntity);

        await materialRepository.AddAsync(materialEntity!);
        await materialRepository.UnitOfWork.CommitAsync(cancellationToken);

        return BusinessResult.Success(materialEntity!.Id);
    }
}