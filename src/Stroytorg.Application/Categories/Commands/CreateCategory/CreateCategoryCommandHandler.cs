using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    ICategoryRepository categoryRepository) :
    IRequestHandler<CreateCategoryCommand, BusinessResponse<int>>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly ICategoryRepository categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

    public async Task<BusinessResponse<int>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var categoryEntity = await categoryRepository.GetByNameAsync(command.Name, cancellationToken);
        if (categoryEntity is not null || cancellationToken.IsCancellationRequested)
        {
            return new BusinessResponse<int>(
            IsSuccess: false,
                BusinessErrorMessage: cancellationToken.IsCancellationRequested ?
                BusinessErrorMessage.OperationCancelled : BusinessErrorMessage.AlreadyExistingEntity
                );
        }

        categoryEntity = autoMapperTypeMapper.Map(command, categoryEntity);

        await categoryRepository.AddAsync(categoryEntity!);
        await categoryRepository.UnitOfWork.Commit();

        return new BusinessResponse<int>(
            Value: categoryEntity!.Id
            );
    }
}
