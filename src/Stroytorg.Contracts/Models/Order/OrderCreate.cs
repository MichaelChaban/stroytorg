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
    [Range(0, 1)]
    int ShippingType,

    [Required]
    [Range(0, 1)]
    int PaymentType,

    [Required]
    [Range(0, 100)]
    ICollection<MaterialMapCreate> MaterialMap,

    string? ShippingAddress);