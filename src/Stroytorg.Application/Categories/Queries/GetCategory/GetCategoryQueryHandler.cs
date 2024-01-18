using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Contracts.Models.Category;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Categories.Queries.GetCategory;

public class GetCategoryQueryHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    ICategoryRepository categoryRepository) :
    IRequestHandler<GetCategoryQuery, BusinessResponse<Category>>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly ICategoryRepository categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

    public async Task<BusinessResponse<Category>> Handle(GetCategoryQuery query, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetAsync(query.CategoryId, cancellationToken);
        if (category is null)
        {
            return new BusinessResponse<Category>(
            IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        return new BusinessResponse<Category>(
            Value: autoMapperTypeMapper.Map<Category>(category));
    }
}
