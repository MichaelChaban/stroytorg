using MediatR;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Material;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Contracts.Sorting;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Application.Materials.Queries.GetPagedMaterial;

public record GetPagedMaterialQuery<TFilter>(
    TFilter? Filter,
    SortDefinition Sort = default!,

    [Range(0, 500)]
    int Offset = 0,

    [Range(0, int.MaxValue)]
    int Limit = 50
    ) : IRequest<PagedData<Material>>
    where TFilter : MaterialFilter;
