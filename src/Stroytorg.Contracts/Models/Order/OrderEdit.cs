using Stroytorg.Contracts.Enums;
using Stroytorg.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Contracts.Models.Order;

public record OrderEdit(
    [Required]
    [EnumValidation(typeof(OrderStatus))]
    OrderStatus OrderStatus);
