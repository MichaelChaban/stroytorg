using Stroytorg.Domain.Specifications.Common;
using Stroytorg.Infrastructure.Specifications.Common;
using Stroytorg.Infrastructure.Specifications.Interfaces;
using Stroytorg.Infrastructure.Specifications;
using System.Linq.Expressions;
using Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Domain.Specifications;

public class OrderSpecification : BaseSpecification, ISpecification<Order>
{
    public string? Fullname { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public int? MinMaterialsAmount { get; set; }

    public int? MaxMaterialsAmount { get; set; }

    public int? MinTotalPrice { get; set; }

    public int? MaxTotalPrice { get; set; }

    public int? ShippingType { get; set; }

    public int? PaymentType { get; set; }

    public Expression<Func<Order, bool>> SatisfiedBy()
    {
        Specification<Order> specification = new TrueSpecification<Order>();

        if (Id != 0)
        {
            specification &= new DirectSpecification<Order>(x => x.Id == Id);
        }

        if (IsActive.HasValue)
        {
            specification &= new DirectSpecification<Order>(x => x.IsActive == IsActive.Value);
        }

        if (!string.IsNullOrEmpty(Fullname))
        {
            specification &= new DirectSpecification<Order>(x => x.FirstName.ToUpper().Contains(Fullname.ToUpper())) ||
                             new DirectSpecification<Order>(x => x.LastName.ToUpper().Contains(Fullname.ToUpper()));
        }

        if (!string.IsNullOrEmpty(Email))
        {
            specification &= new DirectSpecification<Order>(x => x.Email.ToUpper().Contains(Email.ToUpper()));
        }

        if (!string.IsNullOrEmpty(PhoneNumber))
        {
            specification &= new DirectSpecification<Order>(x => x.PhoneNumber.Contains(PhoneNumber));
        }

        if (MinMaterialsAmount.HasValue)
        {
            specification &= new DirectSpecification<Order>(x => x.MaterialsAmount >= MinMaterialsAmount.Value);
        }

        if (MaxMaterialsAmount.HasValue)
        {
            specification &= new DirectSpecification<Order>(x => x.MaterialsAmount <= MaxMaterialsAmount.Value);
        }

        if (MinTotalPrice.HasValue)
        {
            specification &= new DirectSpecification<Order>(x => x.TotalPrice >= MinTotalPrice.Value);
        }

        if (MaxTotalPrice.HasValue)
        {
            specification &= new DirectSpecification<Order>(x => x.TotalPrice <= MaxTotalPrice.Value);
        }

        if (ShippingType.HasValue)
        {
            specification &= new DirectSpecification<Order>(x => (int)x.ShippingType == ShippingType.Value);
        }

        if (PaymentType.HasValue)
        {
            specification &= new DirectSpecification<Order>(x => (int)x.PaymentType == PaymentType.Value);
        }

        return specification.SatisfiedBy();
    }
}