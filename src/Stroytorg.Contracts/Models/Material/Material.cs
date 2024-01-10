using Stroytorg.Contracts.Models.Category;

namespace Stroytorg.Contracts.Models.Material;

public record Material(
    int Id,
    string Name,
    string Description,
    int CategoryId,
    double Price,
    double StockAmount,
    bool IsFavorite,
    double? Height,
    double? Width,
    double? Length,
    double? Weigth,
    Category.Category Category);