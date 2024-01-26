using FluentValidation;
using Stroytorg.Application.Constants;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Categories.GetCategory;

internal class GetCategoryQueryValidator : AbstractValidator<GetCategoryQuery>
{
    private readonly ICategoryRepository categoryRepository;

    public GetCategoryQueryValidator(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

        RuleFor(category => category.CategoryId)
            .MustAsync(CategoryWithIdExistsAsync)
            .WithMessage(BusinessErrorMessage.NotExistingCategoryWithId);
    }

    private async Task<bool> CategoryWithIdExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await categoryRepository.ExistsAsync(id, cancellationToken);
    }
}
