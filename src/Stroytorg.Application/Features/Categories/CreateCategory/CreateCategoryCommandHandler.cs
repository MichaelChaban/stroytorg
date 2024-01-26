using MediatR;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Categories.CreateCategory;

public class CreateCategoryCommandHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    ICategoryRepository categoryRepository) :
    IRequestHandler<CreateCategoryCommand, BusinessResult<int>>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly ICategoryRepository categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

    public async Task<BusinessResult<int>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var categoryEntity = await categoryRepository.GetByNameAsync(command.Name, cancellationToken);

        categoryEntity = autoMapperTypeMapper.Map(command, categoryEntity);

        await categoryRepository.AddAsync(categoryEntity!);
        await categoryRepository.UnitOfWork.CommitAsync(cancellationToken);

        return BusinessResult.Success(categoryEntity!.Id);
    }
}
