using Stroytorg.Domain.Data.Entities;
using Stroytorg.Domain.Specifications.Common;
using Stroytorg.Infrastructure.Specifications;
using Stroytorg.Infrastructure.Specifications.Common;
using Stroytorg.Infrastructure.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Stroytorg.Domain.Specifications;

public class CategorySpecification : BaseSpecification, ISpecification<Category>
{
    public string? Name { get; set; }

    public Expression<Func<Category, bool>> SatisfiedBy()
    {
        Specification<Category> specification = new TrueSpecification<Category>();

        specification &= new DirectSpecification<Category>(x => (IsActive ?? true) == x.IsActive);

        if (Id != 0)
        {
            specification &= new DirectSpecification<Category>(x => x.Id == this.Id);
        }

        if (!string.IsNullOrEmpty(Name))
        {
            specification &= new DirectSpecification<Category>(x =>
                x.Name.Contains(Name, StringComparison.OrdinalIgnoreCase));
        }

        return specification.SatisfiedBy();
    }
}
