namespace Stroytorg.Contracts.Models.Material;

public record Material(
    int Id,
    string Name,
    string Description,
    int CategoryId,
    decimal Price,
    decimal StockAmount,
    decimal? Height,
    decimal? Width,
    decimal? Length,
    decimal? Weight);