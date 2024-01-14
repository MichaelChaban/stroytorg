using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Contracts.Models.Order;

public record OrderEdit(
    [Required]
    int OrderStatus);
