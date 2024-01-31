using Stroytorg.Contracts.Enums;
using Stroytorg.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Contracts.Models.Order;

public record OrderCreate(
    [Required]
    [MaxLength(50)]
    string FirstName,

    [Required]
    [MaxLength(50)]
    string LastName,

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    string Email,

    [Required]
    [PhoneNumber]
    [MaxLength(50)]
    string PhoneNumber,

    [Required]
    [EnumValidation(typeof(ShippingType))]
    ShippingType ShippingType,

    [Required]
    [EnumValidation(typeof(PaymentType))]
    PaymentType PaymentType,

    [Required]
    IEnumerable<MaterialMapCreate> Materials,

    string? ShippingAddress);