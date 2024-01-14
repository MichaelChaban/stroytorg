using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Contracts.Models.Order;

public record MaterialMapCreate(
    [Required]
    int MaterialId,

    [Required]
    int OrderId,

    [Required]
    double TotalMaterialAmount);
