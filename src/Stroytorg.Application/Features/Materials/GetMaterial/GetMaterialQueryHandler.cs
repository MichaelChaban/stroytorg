using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Contracts.Models.Material;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Materials.GetMaterial;

public class GetMaterialQueryHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IMaterialRepository materialRepository)
    : IQueryHandler<GetMaterialQuery, MaterialDetail>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IMaterialRepository materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));

    public async Task<BusinessResult<MaterialDetail>> Handle(GetMaterialQuery query, CancellationToken cancellationToken)
    {
        var material = await materialRepository.GetAsync(query.MaterialId, cancellationToken);

        return BusinessResult.Success(autoMapperTypeMapper.Map<MaterialDetail>(material));
    }
}