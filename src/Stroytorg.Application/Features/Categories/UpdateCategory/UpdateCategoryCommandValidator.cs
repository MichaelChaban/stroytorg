using FluentValidation;
using Stroytorg.Application.Constants;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Categories.UpdateCategory;

internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    private readonly ICategoryRepository categoryRepository;

    public UpdateCategoryCommandValidator(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

        RuleFor(category => category.CategoryId)
            .MustAsync(CategoryWithIdExistsAsync)
            .WithMessage(BusinessErrorMessage.NotExistingCategoryWithId);

        RuleFor(category => category.Name)
            .MustAsync(CategoryWithNameNotExistsAsync)
            .WithMessage(BusinessErrorMessage.ExistingCategoryWithName);
    }

    private async Task<bool> CategoryWithIdExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await categoryRepository.ExistsAsync(id, cancellationToken);
    }

    private async Task<bool> CategoryWithNameNotExistsAsync(string name, CancellationToken cancellationToken)
    {
        return !await categoryRepository.ExistsWithNameAsync(name, cancellationToken);
    }
}
