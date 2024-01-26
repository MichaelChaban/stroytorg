using MediatR;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Categories.UpdateCategory;

public class UpdateCategoryCommandHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    ICategoryRepository categoryRepository) :
    IRequestHandler<UpdateCategoryCommand, BusinessResult<int>>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly ICategoryRepository categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

    public async Task<BusinessResult<int>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var categoryEntity = await categoryRepository.GetAsync(command.CategoryId, cancellationToken);
        categoryEntity = autoMapperTypeMapper.Map(command, categoryEntity);

        categoryRepository.Update(categoryEntity);
        await categoryRepository.UnitOfWork.CommitAsync(cancellationToken);

        return BusinessResult.Success(categoryEntity.Id);
    }
}
