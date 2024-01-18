using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Contracts.Models.Material;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Materials.Queries.GetMaterial;

public class GetMaterialQueryHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IMaterialRepository materialRepository) :
    IRequestHandler<GetMaterialQuery, BusinessResponse<Material>>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IMaterialRepository materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));

    public async Task<BusinessResponse<Material>> Handle(GetMaterialQuery query, CancellationToken cancellationToken)
    {
        var material = await materialRepository.GetAsync(query.MaterialId, cancellationToken);
        if (material is null)
        {
            return new BusinessResponse<Material>(
                IsSuccess: false,
                BusinessErrorMessage: cancellationToken.IsCancellationRequested ?
                BusinessErrorMessage.OperationCancelled : BusinessErrorMessage.NotExistingEntity);
        }

        return new BusinessResponse<Material>(
            Value: autoMapperTypeMapper.Map<Material>(material));
    }
}
