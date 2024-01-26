using FluentValidation;
using Stroytorg.Application.Constants;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Categories.CreateCategory;

internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly ICategoryRepository categoryRepository;

    public CreateCategoryCommandValidator(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

        RuleFor(category => category.Name)
            .MustAsync(CategoryWithNameNotExistsAsync)
            .WithMessage(BusinessErrorMessage.ExistingCategoryWithName);
    }

    private async Task<bool> CategoryWithNameNotExistsAsync(string name, CancellationToken cancellationToken)
    {
        return !await categoryRepository.ExistsWithNameAsync(name, cancellationToken);
    }
}
