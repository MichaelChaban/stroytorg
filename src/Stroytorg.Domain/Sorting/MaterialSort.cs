using Stroytorg.Domain.Sorting.Common;
using System.Linq.Expressions;
using DB = Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Domain.Sorting;

public class MaterialSort : BaseSort<DB.Material>
{
    public MaterialSort(string propertyName, bool isAscending)
        : base(propertyName, isAscending)
    {
    }

    public override Expression<Func<DB.Material, object>> DefaultSort
    {
        get => x => x.Id;
    }

    protected override Expression<Func<DB.Material, object>> GetSortingExpression(string? propertyName) =>
    propertyName switch
    {
        "name" => x => x.Name,
        "category" => x => x.CategoryId,
        "length" => x => x.Length.HasValue ? x.Length.Value : x.Length.HasValue,
        "width" => x => x.Width.HasValue ? x.Width.Value : x.Width.HasValue,
        "height" => x => x.Height.HasValue ? x.Height.Value : x.Height.HasValue,
        "createdDate" => x => x.CreatedAt,
        "updatedDate" => x => x.UpdatedAt.HasValue ? x.UpdatedAt.Value : x.UpdatedAt.HasValue,
        "deactivatedDate" => x => x.DeactivatedAt.HasValue ? x.DeactivatedAt.Value : x.DeactivatedAt.HasValue,
        _ => DefaultSort,
    };
}
