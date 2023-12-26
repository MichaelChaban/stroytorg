using Stroytorg.Domain.Data.Entities.Common;
using System.Linq.Expressions;

namespace Stroytorg.Domain.Sorting;

public abstract class BaseSort<T> where T : IEntity
{
    private readonly bool defaultAsc;
    private Expression<Func<T, object>> defaultSort;

    protected BaseSort(string propertyName, bool isAscending, bool defaultAsc)
    {
        this.PropertyName = propertyName;
        this.IsAscending = isAscending;
        this.defaultAsc = defaultAsc;
    }

    public virtual Expression<Func<T, object>> DefaultSort
    {
        get
        {
            return this.defaultSort ?? (x => x.Id);
        }
        set => this.defaultSort = value;
    }

    public string PropertyName { get; set; }

    public bool IsAscending { get; set; }

    public virtual IQueryable<T> ApplySort(IQueryable<T> query)
    {
        if (string.IsNullOrEmpty(this.PropertyName))
        {
            return this.defaultAsc ? query.OrderBy(this.DefaultSort) : query.OrderByDescending(this.DefaultSort);
        }

        var sortExpression = this.GetSortingExpression(this.PropertyName);
        if (!this.IsAscending)
        {
            return query.OrderByDescending(sortExpression);
        }

        return query.OrderBy(sortExpression);
    }

    protected abstract Expression<Func<T, object>> GetSortingExpression(string propertyName);
}