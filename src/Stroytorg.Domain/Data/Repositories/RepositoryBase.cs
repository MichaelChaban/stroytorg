﻿using Microsoft.EntityFrameworkCore;
using Stroytorg.Domain.Data.Entities;
using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Domain.Sorting.Common;
using Stroytorg.Infrastructure.Store;
using System.Linq.Expressions;
using System.Threading;

namespace Stroytorg.Domain.Data.Repositories;

public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
{
    protected RepositoryBase(IUnitOfWork unitOfWork, IUserContext httpUserContext)
    {
        this.UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        this.HttpUserContext = httpUserContext ?? throw new ArgumentNullException(nameof(httpUserContext));
    }

    public StroytorgDbContext StroytorgContext => (StroytorgDbContext)UnitOfWork;

    public UserContext UserContext => (UserContext)HttpUserContext;

    public IUserContext HttpUserContext { get; }

    public IUnitOfWork UnitOfWork { get; }

    public Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken)
    {
        var query = GetQueryable();
        try
        {
            return query.FirstOrDefaultAsync(x => x.Id!.Equals(id), cancellationToken)!;
        }
        catch (OperationCanceledException)
        {
            return null!;
        }
    }

    public async Task<IEnumerable<TEntity>> GetByIdsAsync(TKey[] ids, CancellationToken cancellationToken)
    {
        var query = GetQueryable();
        try
        {
            return await query.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            return Enumerable.Empty<TEntity>();
        }
    }

    public async Task<IEnumerable<TEntity>> GetPagedAsync(int offset, int limit, Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
    {
        var query = FilterData(filter);
        try
        {
            return await query.Skip(offset).Take(limit).ToArrayAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            return Enumerable.Empty<TEntity>();
        }
    }

    public async Task<IEnumerable<TEntity>> GetPagedSortAsync<TSort>(int offset, int limit, Expression<Func<TEntity, bool>> filter, SortDefinition sort, CancellationToken cancellationToken)
        where TSort : BaseSort<TEntity>
    {
        var query = FilterData(filter);

        var sortField = sort?.Field?.ToLower();
        var sortByAsc = sort?.Direction == null || sort.Direction == SortDirection.Ascending;
        var entitySort = Activator.CreateInstance(typeof(TSort), sortField, sortByAsc) as TSort;
        query = entitySort!.ApplySort(query);

        try
        {
            return await query.Skip(offset).Take(limit).ToArrayAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            return Enumerable.Empty<TEntity>();
        }
    }

    public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
    {
        var query = FilterData(filter);
        try
        {
            var result = await query.CountAsync(cancellationToken);
            return result;
        }
        catch (OperationCanceledException)
        {
            return -1;
        }
    }

    public async Task<IEnumerable<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
    {
        var query = FilterData(filter);
        try
        {
            return await query.ToArrayAsync(cancellationToken);
        }
        catch (Exception)
        {
            return Enumerable.Empty<TEntity>();
        }
    }

    public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            return await FilterData(null).ToArrayAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            return Enumerable.Empty<TEntity>();
        }
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        if (entity is Auditable auditableEntity)
        {
            var fullName = HttpUserContext.User.Identity?.Name;
            auditableEntity.CreatedAt = DateTimeOffset.UtcNow;
            auditableEntity.CreatedBy = !string.IsNullOrEmpty(fullName) ? fullName : "System";
        }

        _ = await GetDbSet().AddAsync(entity);
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity is Auditable auditableEntity)
            {
                var fullName = HttpUserContext.User.Identity?.Name;
                auditableEntity.UpdatedAt = DateTimeOffset.UtcNow;
                auditableEntity.UpdatedBy = !string.IsNullOrEmpty(fullName) ? fullName : "System";
            }
        }

        GetDbSet().UpdateRange(entities);
    }

    public virtual void Update(TEntity entity)
    {
        if (entity is Auditable auditableEntity)
        {
            var fullName = HttpUserContext.User.Identity?.Name;
            auditableEntity.UpdatedAt = DateTimeOffset.UtcNow;
            auditableEntity.UpdatedBy = !string.IsNullOrEmpty(fullName) ? fullName : "System";
        }

        _ = GetDbSet().Update(entity);
    }

    public virtual void Deactivate(TEntity entity)
    {
        entity.IsActive = false;
        if (entity is Auditable auditableEntity)
        {
            var fullName = HttpUserContext.User.Identity?.Name;
            auditableEntity.DeactivatedAt = DateTimeOffset.UtcNow;
            auditableEntity.DeactivatedBy = !string.IsNullOrEmpty(fullName) ? fullName : "System";
        }

        Update(entity);
    }

    public virtual void DeactivateRange(TEntity[] entities)
    {
        foreach (var entity in entities)
        {
            entity.IsActive = false;
            if (entity is Auditable auditableEntity)
            {
                var fullName = HttpUserContext.User.Identity?.Name;
                auditableEntity.DeactivatedAt = DateTimeOffset.UtcNow;
                auditableEntity.DeactivatedBy = !string.IsNullOrEmpty(fullName) ? fullName : "System";
            }
        }

        UpdateRange(entities);
    }

    public virtual void Remove(TEntity entity)
    {
        _ = GetDbSet().Remove(entity);
    }

    public virtual void RemoveRange(TEntity[] entities)
    {
        GetDbSet().RemoveRange(entities);
    }

    protected virtual IQueryable<TEntity> GetQueryable()
    {
        return GetDbSet();
    }

    protected abstract DbSet<TEntity> GetDbSet();

    protected IQueryable<TEntity> FilterData(Expression<Func<TEntity, bool>>? filter)
    {
        IQueryable<TEntity> query = GetQueryable();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        return query;
    }

    protected IQueryable<TEntity> FilterData(IQueryable<TEntity> query, Expression<Func<TEntity, bool>>? filter)
    {
        if (filter != null)
        {
            query = query.Where(filter);
        }

        return query;
    }
}