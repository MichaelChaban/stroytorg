using Stroytorg.Domain.Data.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Domain.Data.Entities;

public class Material : Auditable
{
    [Required]
    [MaxLength(150)]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    public int CategoryId { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public double Price { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public double StockAmount { get; set; }

    public double? Height { get; set; }

    public double? Width { get; set; }

    public double? Length { get; set; }

    public double? Weight { get; set; }

    public bool IsFavorite { get; set; } = false;

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderMaterialMap>? OrderMaterialMap { get; set; }
}
