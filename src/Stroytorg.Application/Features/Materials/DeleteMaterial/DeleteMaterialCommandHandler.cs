using MediatR;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Materials.DeleteMaterial;

public class DeleteMaterialCommandHandler(
    IMaterialRepository materialRepository)
    : IRequestHandler<DeleteMaterialCommand, BusinessResult<int>>
{
    private readonly IMaterialRepository materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));

    public async Task<BusinessResult<int>> Handle(DeleteMaterialCommand command, CancellationToken cancellationToken)
    {
        var materialEntity = await materialRepository.GetAsync(command.MaterialId, cancellationToken);

        materialRepository.Deactivate(materialEntity);
        await materialRepository.UnitOfWork.CommitAsync(cancellationToken);

        return BusinessResult.Success(materialEntity.Id);
    }
}