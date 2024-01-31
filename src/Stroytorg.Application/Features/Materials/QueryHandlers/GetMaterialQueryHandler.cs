using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Features.Materials.Queries;
using Stroytorg.Contracts.Models.Material;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Features.Materials.QueryHandlers;

public class GetMaterialQueryHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IMaterialRepository materialRepository) :
    IRequestHandler<GetMaterialQuery, BusinessResponse<MaterialDetail>>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IMaterialRepository materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));

    public async Task<BusinessResponse<MaterialDetail>> Handle(GetMaterialQuery query, CancellationToken cancellationToken)
    {
        var material = await materialRepository.GetAsync(query.MaterialId, cancellationToken);
        if (material is null)
        {
            return new BusinessResponse<MaterialDetail>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        return new BusinessResponse<MaterialDetail>(
            Value: autoMapperTypeMapper.Map<MaterialDetail>(material));
    }
}