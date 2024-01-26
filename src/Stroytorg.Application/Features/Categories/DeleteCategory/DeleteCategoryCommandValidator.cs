using FluentValidation;
using Stroytorg.Application.Constants;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Categories.DeleteCategory;

internal class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMaterialRepository materialRepository;

    public DeleteCategoryCommandValidator(
        ICategoryRepository categoryRepository,
        IMaterialRepository materialRepository)
    {
        this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        this.materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));

        RuleFor(category => category.CategoryId)
            .MustAsync(CategoryWithIdExistsAsync)
            .WithMessage(BusinessErrorMessage.NotExistingCategoryWithId);

        RuleFor(category => category.CategoryId)
            .MustAsync(CategoryMaterialsNotExistAsync)
            .WithMessage(BusinessErrorMessage.ExistingMaterialsWithCategoryId);
    }

    private async Task<bool> CategoryWithIdExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await categoryRepository.ExistsAsync(id, cancellationToken);
    }

    private async Task<bool> CategoryMaterialsNotExistAsync(int id, CancellationToken cancellationToken)
    {
        return !await materialRepository.ExistsWithCategoryIdAsync(id, cancellationToken);
    }
}