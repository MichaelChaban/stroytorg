using Stroytorg.Domain.Sorting.Common;
using System.Linq.Expressions;
using DB = Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Domain.Sorting;

public class CategorySort : BaseSort<DB.Category>
{
    public CategorySort(string propertyName, bool isAscending, bool defaultAsc)
        : base(propertyName, isAscending, defaultAsc)
    {
    }

    public override Expression<Func<DB.Category, object>> DefaultSort
    {
        get => x => x.Name;
    }

    protected override Expression<Func<DB.Category, object>> GetSortingExpression(string propertyName) =>
    propertyName switch
    {
        "id" => x => x.Id,
        _ => DefaultSort,
    };
}