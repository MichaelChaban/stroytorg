using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Features.Categories.Commands;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Features.Categories.CommandHandlers;

public class UpdateCategoryCommandHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    ICategoryRepository categoryRepository) :
    IRequestHandler<UpdateCategoryCommand, BusinessResponse<int>>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly ICategoryRepository categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

    public async Task<BusinessResponse<int>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var categoryEntity = await categoryRepository.GetAsync(command.CategoryId, cancellationToken);
        if (categoryEntity is null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        categoryEntity = autoMapperTypeMapper.Map(command.Category, categoryEntity);

        categoryRepository.Update(categoryEntity);
        await categoryRepository.UnitOfWork.CommitAsync(cancellationToken);

        return new BusinessResponse<int>(
            Value: categoryEntity.Id);
    }
}
