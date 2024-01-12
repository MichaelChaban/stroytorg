using Stroytorg.Contracts.Sorting;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Contracts.RequestModels;

public record DataRangeRequest<TFilter>(
    TFilter? Filter,
    SortDefinition Sort = default!,

    [Range(0, 500)]
    int Offset = 0,

    [Range(0, int.MaxValue)]
    int Limit = 50);
