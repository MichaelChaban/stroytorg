using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Sorting;
using Stroytorg.Infrastructure.Store;
using System.Linq.Expressions;

namespace Stroytorg.Domain.Data.Repositories.Interfaces;

public interface IRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
{
    IUnitOfWork UnitOfWork { get; }

    Task<TEntity> GetAsync(TKey id);

    Task<IEnumerable<TEntity>> GetByIdsAsync(TKey[] ids);

    Task<IEnumerable<TEntity>> GetPagedAsync(int offset, int limit, Expression<Func<TEntity, bool>> filter);

    Task<IEnumerable<TEntity>> GetPagedSortAsync<TSort>(int offset, int limit, Expression<Func<TEntity, bool>> filter, SortDefinition sort, bool isAscendingSortByDefault = true)
        where TSort : BaseSort<TEntity>;

    Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter);

    Task<IEnumerable<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter);

    Task<IEnumerable<TEntity>> GetAll();

    Task AddAsync(TEntity entity);

    void Update(TEntity entity);

    void UpdateRange(IEnumerable<TEntity> entity);

    void Remove(TEntity entity);

    void RemoveRange(TEntity[] entity);
}
