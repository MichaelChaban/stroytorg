using MediatR;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Material;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Domain.Sorting;
using Stroytorg.Domain.Sorting.Common;
using Stroytorg.Domain.Specifications;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Materials.Queries.GetPagedMaterial;

public class GetPagedMaterialQueryHandler(
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IMaterialRepository materialRepository
    ) :
    IRequestHandler<GetPagedMaterialQuery<MaterialFilter>, PagedData<Material>>
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IMaterialRepository materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));

    public async Task<PagedData<Material>> Handle(GetPagedMaterialQuery<MaterialFilter> request, CancellationToken cancellationToken)
    {
        var specification = autoMapperTypeMapper.Map<MaterialSpecification>(request?.Filter!);
        var filter = specification?.SatisfiedBy();

        var totalItems = await materialRepository.GetCountAsync(filter!, cancellationToken);
        var items = await materialRepository.GetPagedSortAsync<MaterialSort>(request!.Offset, request.Limit, filter!, autoMapperTypeMapper.Map<SortDefinition>(request.Sort), cancellationToken);
        var mappedItems = autoMapperTypeMapper.Map<Material>(items);

        return new PagedData<Material>(
            Data: mappedItems,
            Total: totalItems
            );
    }
}
