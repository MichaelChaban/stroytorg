using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Domain.Data.Entities;

public class Order : Auditable
{
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public int? UserId { get; set; }

    [Required]
    public int MaterialsAmount { get; set; }

    [Required]
    public double TotalPrice { get; set; }

    public ShippingTypeEnum ShippingType { get; set; }

    public int ShippingAddressId { get; set; }

    public PaymentTypeEnum PaymentType { get; set; }

    public OrderStatusEnum OrderStatus { get; set; }

    public virtual User? User {  get; set; }

    public virtual ICollection<OrderMaterialMap>? OrderMaterialMap { get; set; }

    public virtual Address? ShippingAddress { get; set; }
}
