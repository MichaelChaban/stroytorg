using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Contracts.Models.Order;

public record MaterialMapCreate(
    [Required]
    int MaterialId,

    [Required]
    [Range(0, 100000)]
    decimal TotalMaterialAmount);