using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Categories.Commands.UpdateCategory;

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
                BusinessErrorMessage: cancellationToken.IsCancellationRequested ?
                BusinessErrorMessage.OperationCancelled : BusinessErrorMessage.NotExistingEntity
                );
        }

        categoryEntity = autoMapperTypeMapper.Map(command, categoryEntity);

        categoryRepository.Update(categoryEntity);
        await categoryRepository.UnitOfWork.Commit();

        return new BusinessResponse<int>(
            Value: categoryEntity.Id
            );
    }
}
