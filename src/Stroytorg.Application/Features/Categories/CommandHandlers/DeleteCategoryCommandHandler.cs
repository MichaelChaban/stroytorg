﻿using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Features.Categories.Commands;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Categories.CommandHandlers;

public class DeleteCategoryCommandHandler(
    ICategoryRepository categoryRepository) :
    IRequestHandler<DeleteCategoryCommand, BusinessResponse<int>>
{
    private readonly ICategoryRepository categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

    public async Task<BusinessResponse<int>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var categoryEntity = await categoryRepository.GetAsync(command.CategoryId, cancellationToken);
        if (categoryEntity is null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        if (categoryEntity.Materials?.Count > 0)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.UnableToDeleteEntity);
        }

        categoryRepository.Remove(categoryEntity);
        await categoryRepository.UnitOfWork.CommitAsync(cancellationToken);

        return new BusinessResponse<int>(
            Value: categoryEntity.Id);
    }
}