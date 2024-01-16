using System.Linq.Expressions;

namespace Stroytorg.Infrastructure.Specifications.Common;

public sealed class TrueSpecification<TEntity> : Specification<TEntity>
        where TEntity : class
{
    public override Expression<Func<TEntity, bool>> SatisfiedBy()
    {
        const bool result = true;

        Expression<Func<TEntity, bool>> trueExpression = t => result;
        return trueExpression;
    }
}