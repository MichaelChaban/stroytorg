﻿using Stroytorg.Domain.Data.Entities.Common;
using System.Linq.Expressions;

namespace Stroytorg.Domain.Sorting.Common;

public abstract class BaseSort<T> where T : IEntity
{
    private readonly bool defaultAsc;
    private Expression<Func<T, object>>? defaultSort;

    protected BaseSort(string propertyName, bool isAscending, bool defaultAsc)
    {
        PropertyName = propertyName;
        IsAscending = isAscending;
        this.defaultAsc = defaultAsc;
    }

    public virtual Expression<Func<T, object>> DefaultSort
    {
        get
        {
            return defaultSort ?? (x => x.Id);
        }
        set => defaultSort = value;
    }

    public string PropertyName { get; set; }

    public bool IsAscending { get; set; }

    public virtual IQueryable<T> ApplySort(IQueryable<T> query)
    {
        if (string.IsNullOrEmpty(PropertyName))
        {
            return defaultAsc ? query.OrderBy(DefaultSort) : query.OrderByDescending(DefaultSort);
        }

        var sortExpression = GetSortingExpression(PropertyName.ToLower());
        if (!IsAscending)
        {
            return query.OrderByDescending(sortExpression);
        }

        return query.OrderBy(sortExpression);
    }

    protected abstract Expression<Func<T, object>> GetSortingExpression(string propertyName);
}