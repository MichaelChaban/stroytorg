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
    [Range(0, 100000)]
    decimal Price,

    [Required]
    [Range(0, 100000)]
    decimal StockAmount,

    [Range(0, 100000)]
    decimal? Height,

    [Range(0, 100000)]
    decimal? Width,

    [Range(0, 100000)]
    decimal? Length,

    [Range(0, 100000)]
    decimal? Weight);