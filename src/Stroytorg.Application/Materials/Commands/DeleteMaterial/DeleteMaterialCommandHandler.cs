﻿using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Materials.Commands.DeleteMaterial;

public class DeleteMaterialCommandHandler(
    IMaterialRepository materialRepository) :
    IRequestHandler<DeleteMaterialCommand, BusinessResponse<int>>
{
    private readonly IMaterialRepository materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));

    public async Task<BusinessResponse<int>> Handle(DeleteMaterialCommand command, CancellationToken cancellationToken)
    {
        var materialEntity = await materialRepository.GetAsync(command.MaterialId, cancellationToken);
        if (materialEntity is null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        materialRepository.Deactivate(materialEntity);
        await materialRepository.UnitOfWork.Commit(cancellationToken);

        return new BusinessResponse<int>(
            Value: materialEntity.Id);
    }
}