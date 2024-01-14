using Stroytorg.Domain.Sorting.Common;
using System.Linq.Expressions;
using DB = Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Domain.Sorting;

public class OrderSort : BaseSort<DB.Order>
{
    public OrderSort(string propertyName, bool isAscending)
        : base(propertyName, isAscending)
    {
    }

    public override Expression<Func<DB.Order, object>> DefaultSort
    {
        get => x => x.Id;
    }

    protected override Expression<Func<DB.Order, object>> GetSortingExpression(string? propertyName) =>
    propertyName switch
    {
        "firstname" => x => x.FirstName,
        "lastname" => x => x.LastName,
        "email" => x => x.Email,
        "userid" => x => x.UserId.HasValue ? x.UserId.Value : x.UserId.HasValue,
        "materialsamount" => x => x.MaterialsAmount,
        "totalprice" => x => x.TotalPrice,
        "shippingtype" => x => x.ShippingType,
        "shippingaddress" => x => !string.IsNullOrEmpty(x.ShippingAddress) ? x.ShippingAddress : !string.IsNullOrEmpty(x.ShippingAddress),
        "paymenttype" => x => x.PaymentType,
        "orderstatus" => x => x.OrderStatus,
        "createdDate" => x => x.CreatedAt,
        "updatedDate" => x => x.UpdatedAt.HasValue ? x.UpdatedAt.Value : x.UpdatedAt.HasValue,
        "deactivatedDate" => x => x.DeactivatedAt.HasValue ? x.DeactivatedAt.Value : x.DeactivatedAt.HasValue,
        _ => DefaultSort,
    };
}