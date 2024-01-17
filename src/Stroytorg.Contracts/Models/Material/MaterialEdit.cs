using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Contracts.Models.Material;

public record MaterialEdit(
    [Required]
    [MinLength(5)]
    [MaxLength(150)]
    string Name,

    [Required]
    [MinLength(20)]
    string Description,

    [Required]
    int CategoryId,

    [Required]
    [Range(0, double.MaxValue)]
    double Price,

    [Required]
    [Range(0, double.MaxValue)]
    double StockAmount,

    [Range(0, double.MaxValue)]
    double? Height,

    [Range(0, double.MaxValue)]
    double? Width,

    [Range(0, double.MaxValue)]
    double? Length,

    [Range(0, double.MaxValue)]
    double? Weight,
    bool? IsFavorite);