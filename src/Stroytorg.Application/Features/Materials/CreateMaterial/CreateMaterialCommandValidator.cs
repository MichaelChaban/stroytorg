using FluentValidation;
using Stroytorg.Application.Constants;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Materials.CreateMaterial;

internal class CreateMaterialCommandValidator : AbstractValidator<CreateMaterialCommand>
{
    private readonly IMaterialRepository materialRepository;
    private readonly ICategoryRepository categoryRepository;

    public CreateMaterialCommandValidator(
        IMaterialRepository materialRepository,
        ICategoryRepository categoryRepository)
    {
        this.materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
        this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

        RuleFor(material => material.Name)
            .MustAsync(MaterialWithNameNotExistsAsync)
            .WithMessage(BusinessErrorMessage.ExistingMaterialsWithName);

        RuleFor(material => material.CategoryId)
            .MustAsync(CategoryWithIdExistsAsync)
            .WithMessage(BusinessErrorMessage.NotExistingCategoryWithId);
    }

    private async Task<bool> MaterialWithNameNotExistsAsync(string name, CancellationToken cancellationToken)
    {
        return !await materialRepository.ExistsWithNameAsync(name, cancellationToken);
    }

    private async Task<bool> CategoryWithIdExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await categoryRepository.ExistsAsync(id, cancellationToken);
    }
}
