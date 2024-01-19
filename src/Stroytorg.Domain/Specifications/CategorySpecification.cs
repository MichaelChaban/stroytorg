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

        if (Id != 0)
        {
            specification &= new DirectSpecification<Category>(x => x.Id == Id);
        }

        if (IsActive.HasValue)
        {
            specification &= new DirectSpecification<Category>(x => x.IsActive == IsActive.Value);
        }

        if (!string.IsNullOrEmpty(Name))
        {
            specification &= new DirectSpecification<Category>(x =>
                x.Name.ToUpper().Contains(Name.ToUpper()));
        }

        return specification.SatisfiedBy();
    }
}
