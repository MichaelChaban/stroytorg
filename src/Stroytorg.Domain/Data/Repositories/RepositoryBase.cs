using Microsoft.EntityFrameworkCore;
using Stroytorg.Domain.Data.Entities;
using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Domain.Sorting;
using Stroytorg.Infrastructure.Store;
using System.Linq.Expressions;

namespace Stroytorg.Domain.Data.Repositories;

public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
{
    protected RepositoryBase(IUnitOfWork unitOfWork, IUserContext httpUserContext)
    {
        this.UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        this.HttpUserContext = httpUserContext ?? throw new ArgumentNullException(nameof(httpUserContext));
    }

    public StroytorgDbContext StroytorgContext => (StroytorgDbContext)this.UnitOfWork;

    public UserContext UserContext => (UserContext)this.HttpUserContext;

    public IUserContext HttpUserContext { get; }

    public IUnitOfWork UnitOfWork { get; }

    public Task<TEntity> GetAsync(TKey id)
    {
        var query = this.GetQueryable();
        return query.FirstOrDefaultAsync(x => x.Id!.Equals(id))!;
    }

    public async Task<IEnumerable<TEntity>> GetByIdsAsync(TKey[] ids)
    {
        var query = this.GetQueryable();
        return await query.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetPagedAsync(int offset, int limit, Expression<Func<TEntity, bool>> filter)
    {
        var query = this.FilterData(filter);

        return await query.Skip(offset).Take(limit).ToArrayAsync();
    }

    public async Task<IEnumerable<TEntity>> GetPagedSortAsync<TSort>(int offset, int limit, Expression<Func<TEntity, bool>> filter, SortDefinition sort, bool isAscendingSortByDefault = true)
        where TSort : BaseSort<TEntity>
    {
        var query = this.FilterData(filter);

        var sortField = sort?.Field.ToLower();
        var sortByAsc = sort?.Direction == null || sort.Direction == SortDirection.Asc;
        var entitySort = Activator.CreateInstance(typeof(TSort), sortField, sortByAsc, isAscendingSortByDefault) as TSort;
        query = entitySort!.ApplySort(query);

        return await query.Skip(offset).Take(limit).ToArrayAsync();
    }

    public Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter)
    {
        var query = this.FilterData(filter);

        return query.CountAsync();
    }

    public async Task<IEnumerable<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter)
    {
        var query = this.FilterData(filter);
        return await query.ToArrayAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await this.FilterData(null).ToArrayAsync();
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        if (entity is Auditable auditableEntity)
        {
            var fullName = HttpUserContext.User.Identity?.Name;
            auditableEntity.CreatedAt = DateTimeOffset.UtcNow;
            auditableEntity.CreatedBy = !string.IsNullOrEmpty(fullName) ? fullName : "System";
        }

        _ = await this.GetDbSet().AddAsync(entity);
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

        this.GetDbSet().UpdateRange(entities);
    }

    public virtual void Update(TEntity entity)
    {
        if (entity is Auditable auditableEntity)
        {
            var fullName = HttpUserContext.User.Identity?.Name;
            auditableEntity.UpdatedAt = DateTimeOffset.UtcNow;
            auditableEntity.UpdatedBy = !string.IsNullOrEmpty(fullName) ? fullName : "System";
        }

        _ = this.GetDbSet().Update(entity);
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
        _ = this.GetDbSet().Remove(entity);
    }

    public virtual void RemoveRange(TEntity[] entities)
    {
        this.GetDbSet().RemoveRange(entities);
    }

    protected virtual IQueryable<TEntity> GetQueryable()
    {
        return this.GetDbSet();
    }

    protected abstract DbSet<TEntity> GetDbSet();

    protected IQueryable<TEntity> FilterData(Expression<Func<TEntity, bool>>? filter)
    {
        IQueryable<TEntity> query = this.GetQueryable();
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