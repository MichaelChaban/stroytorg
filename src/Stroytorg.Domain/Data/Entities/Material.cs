using Stroytorg.Domain.Data.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Domain.Data.Entities;

public class Material : Auditable
{
    [Required]
    [MinLength(5)]
    [MaxLength(150)]
    public required string Name { get; set; }

    [Required]
    [MinLength(20)]
    public required string Description { get; set; }

    public int CategoryId { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public double Price { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public double StockAmount { get; set; }

    [Range(0, double.MaxValue)]
    public double? Height { get; set; }

    [Range(0, double.MaxValue)]
    public double? Width { get; set; }

    [Range(0, double.MaxValue)]
    public double? Length { get; set; }

    [Range(0, double.MaxValue)]
    public double? Weight { get; set; }

    public bool IsFavorite { get; set; } = false;

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderMaterialMap>? OrderMaterialMap { get; set; }
}
