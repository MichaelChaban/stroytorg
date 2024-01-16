using System.Linq.Expressions;

namespace Stroytorg.Infrastructure.Specifications.Interfaces;

public interface ISpecification<TEntity>
        where TEntity : class
{
    Expression<Func<TEntity, bool>> SatisfiedBy();
}