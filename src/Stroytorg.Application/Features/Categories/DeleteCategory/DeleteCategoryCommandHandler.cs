using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Categories.DeleteCategory;

public class DeleteCategoryCommandHandler(
    ICategoryRepository categoryRepository) :
    ICommandHandler<DeleteCategoryCommand, int>
{
    private readonly ICategoryRepository categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

    public async Task<BusinessResult<int>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var categoryEntity = await categoryRepository.GetAsync(command.CategoryId, cancellationToken);

        categoryRepository.Remove(categoryEntity);
        await categoryRepository.UnitOfWork.CommitAsync(cancellationToken);

        return BusinessResult.Success(categoryEntity.Id);
    }
}
