using FluentValidation;
using Stroytorg.Application.Constants;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Materials.DeleteMaterial;

internal class DeleteMaterialCommandValidator : AbstractValidator<DeleteMaterialCommand>
{
    private readonly IMaterialRepository materialRepository;

    public DeleteMaterialCommandValidator(IMaterialRepository materialRepository)
    {
        this.materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));

        RuleFor(material => material.MaterialId)
            .MustAsync(MaterialWithIdExistsAsync)
            .WithErrorCode(nameof(DeleteMaterialCommand.MaterialId))
            .WithMessage(BusinessErrorMessage.NotExistingMaterialWithId);
    }

    private async Task<bool> MaterialWithIdExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await materialRepository.ExistsAsync(id, cancellationToken);
    }
}
