using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Domain.Data.Entities;

public class Order : Auditable
{
    [Required]
    [MaxLength(50)]
    public required string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public required string LastName { get; set; }

    [Required]
    [MaxLength(150)]
    public required string Email { get; set; }

    [Required]
    [MaxLength(50)]
    public required string PhoneNumber { get; set; }

    public int? UserId { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int MaterialsAmount { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public double TotalPrice { get; set; }

    [Required]
    public ShippingType ShippingType { get; set; }

    [MaxLength(200)]
    public string? ShippingAddress { get; set; }

    [Required]
    public PaymentType PaymentType { get; set; }

    public OrderStatus OrderStatus { get; set; } = OrderStatus.NewOrder;

    public virtual User? User {  get; set; }

    public virtual ICollection<OrderMaterialMap>? OrderMaterialMap { get; set; }
}
