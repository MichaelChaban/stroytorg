using Stroytorg.Infrastructure.Specifications.Interfaces;

namespace Stroytorg.Infrastructure.Specifications.Common;

public abstract class CompositeSpecification<TEntity>
        : Specification<TEntity>
        where TEntity : class
{
    public abstract ISpecification<TEntity> LeftSideSpecification { get; }

    public abstract ISpecification<TEntity> RightSideSpecification { get; }
}