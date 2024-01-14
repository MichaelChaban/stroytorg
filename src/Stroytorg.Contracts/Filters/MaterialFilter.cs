namespace Stroytorg.Contracts.Filters;

public record MaterialFilter(
    int? Id,
    string? Name,
    int? CategoryId,
    double? MinPrice,
    double? MaxPrice,
    double? MinStockAmount,
    double? MaxStockAmount,
    bool? IsFavorite,
    double? MinHeight,
    double? MaxHeight,
    double? MinWidth,
    double? MaxWidth,
    double? MinLength,
    double? MaxLength,
    double? MinWeight,
    double? MaxWeight,
    bool? IsActive);