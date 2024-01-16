using Stroytorg.Infrastructure.Specifications.Common;
using Stroytorg.Infrastructure.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Stroytorg.Infrastructure.Specifications;

public sealed class OrSpecification<T>
        : CompositeSpecification<T>
        where T : class
{
    public OrSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
    {
        this.LeftSideSpecification = leftSide ?? throw new ArgumentNullException(nameof(leftSide));
        this.RightSideSpecification = rightSide ?? throw new ArgumentNullException(nameof(rightSide));
    }

    public override ISpecification<T> LeftSideSpecification { get; }

    public override ISpecification<T> RightSideSpecification { get; }

    public override Expression<Func<T, bool>> SatisfiedBy()
    {
        Expression<Func<T, bool>> left = this.LeftSideSpecification.SatisfiedBy();
        Expression<Func<T, bool>> right = this.RightSideSpecification.SatisfiedBy();
        return left.Or(right);
    }
}
