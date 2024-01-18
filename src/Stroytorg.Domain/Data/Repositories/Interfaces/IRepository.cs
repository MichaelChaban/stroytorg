using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Sorting.Common;
using Stroytorg.Infrastructure.Store;
using System.Linq.Expressions;

namespace Stroytorg.Domain.Data.Repositories.Interfaces;

public interface IRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
{
    IUnitOfWork UnitOfWork { get; }

    Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> GetByIdsAsync(TKey[] ids, CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> GetPagedAsync(int offset, int limit, Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> GetPagedSortAsync<TSort>(int offset, int limit, Expression<Func<TEntity, bool>> filter, SortDefinition sort, CancellationToken cancellationToken)
        where TSort : BaseSort<TEntity>;

    Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken);

    Task AddAsync(TEntity entity);

    void Update(TEntity entity);

    void UpdateRange(IEnumerable<TEntity> entity);

    void Remove(TEntity entity);

    void RemoveRange(TEntity[] entity);

    void Deactivate(TEntity entity);

    void DeactivateRange(TEntity[] entity);
}
