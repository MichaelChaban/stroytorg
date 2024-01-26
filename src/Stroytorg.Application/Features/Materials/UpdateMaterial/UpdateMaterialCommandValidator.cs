using FluentValidation;
using Stroytorg.Application.Constants;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Materials.UpdateMaterial;

internal class UpdateMaterialCommandValidator : AbstractValidator<UpdateMaterialCommand>
{
    private readonly IMaterialRepository materialRepository;
    private readonly ICategoryRepository categoryRepository;

    public UpdateMaterialCommandValidator(
        IMaterialRepository materialRepository,
        ICategoryRepository categoryRepository)
    {
        this.materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
        this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

        RuleFor(material => material.MaterialId)
            .MustAsync(MaterialWithIdExistsAsync)
            .WithMessage(BusinessErrorMessage.NotExistingMaterialWithId);

        RuleFor(material => material.CategoryId)
            .MustAsync(CategoryWithIdExistsAsync)
            .WithMessage(BusinessErrorMessage.NotExistingCategoryWithId);
    }

    private async Task<bool> MaterialWithIdExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await materialRepository.ExistsAsync(id, cancellationToken);
    }

    private async Task<bool> CategoryWithIdExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await categoryRepository.ExistsAsync(id, cancellationToken);
    }
}
