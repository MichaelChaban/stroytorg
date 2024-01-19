using MediatR;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Category;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Domain.Sorting;
using Stroytorg.Domain.Sorting.Common;
using Stroytorg.Domain.Specifications;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Categories.Queries.GetPagedCategory;

public class GetPagedCategoryQueryHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    ICategoryRepository categoryRepository
    ) :
    IRequestHandler<GetPagedCategoryQuery<CategoryFilter>, PagedData<Category>>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly ICategoryRepository categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

    public async Task<PagedData<Category>> Handle(GetPagedCategoryQuery<CategoryFilter> request, CancellationToken cancellationToken)
    {
        var specification = autoMapperTypeMapper.Map<CategorySpecification>(request?.Filter!);
        var filter = specification?.SatisfiedBy();

        var totalItems = await categoryRepository.GetCountAsync(filter!, cancellationToken);
        var items = await categoryRepository.GetPagedSortAsync<CategorySort>(request!.Offset, request.Limit, filter!, autoMapperTypeMapper.Map<SortDefinition>(request.Sort), cancellationToken);
        var mappedItems = autoMapperTypeMapper.Map<Category>(items);

        return new PagedData<Category>(
            Data: mappedItems,
            Total: totalItems
            );
    }
}
