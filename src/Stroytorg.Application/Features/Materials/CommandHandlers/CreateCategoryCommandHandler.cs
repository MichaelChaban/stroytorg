using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Features.Materials.Commands;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Features.Materials.CommandHandlers;

public class CreateMaterialCommandHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IMaterialRepository materialRepository,
    ICategoryRepository categoryRepository) :
    IRequestHandler<CreateMaterialCommand, BusinessResponse<int>>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IMaterialRepository materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));

    public async Task<BusinessResponse<int>> Handle(CreateMaterialCommand command, CancellationToken cancellationToken)
    {
        var materialEntity = await materialRepository.GetByNameAsync(command.Material.Name, cancellationToken);
        if (materialEntity is not null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.AlreadyExistingEntity);
        }

        var category = await categoryRepository.GetAsync(command.Material.CategoryId, cancellationToken);
        if (category is null)
        {
            return new BusinessResponse<int>(
               IsSuccess: false,
               BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        materialEntity = autoMapperTypeMapper.Map(command.Material, materialEntity);

        await materialRepository.AddAsync(materialEntity!);
        await materialRepository.UnitOfWork.CommitAsync(cancellationToken);

        return new BusinessResponse<int>(
            Value: materialEntity!.Id);
    }
}