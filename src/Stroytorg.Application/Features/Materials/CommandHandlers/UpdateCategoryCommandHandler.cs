using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Features.Materials.Commands;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Features.Materials.CommandHandlers;

public class UpdateCategoryCommandHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IMaterialRepository materialRepository,
    ICategoryRepository categoryRepository) :
    IRequestHandler<UpdateMaterialCommand, BusinessResponse<int>>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IMaterialRepository materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
    private readonly ICategoryRepository categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

    public async Task<BusinessResponse<int>> Handle(UpdateMaterialCommand command, CancellationToken cancellationToken)
    {
        var materialEntity = await materialRepository.GetAsync(command.MaterialId, cancellationToken);
        if (materialEntity is null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        var category = await categoryRepository.GetAsync(command.Material.CategoryId, cancellationToken);
        if (category is null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        materialEntity = autoMapperTypeMapper.Map(command.Material, materialEntity);

        materialRepository.Update(materialEntity);
        await materialRepository.UnitOfWork.CommitAsync(cancellationToken);

        return new BusinessResponse<int>(
            Value: materialEntity.Id);
    }
}
