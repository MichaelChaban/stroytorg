using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Contracts.Models.Material;

public record MaterialEdit(
    [MinLength(5)]
    [MaxLength(150)]
    string Name,

    [MinLength(20)]
    string Description,

    int CategoryId,

    [Range(0, double.MaxValue)]
    double Price,

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