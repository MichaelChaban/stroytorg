using Stroytorg.Application.Abstractions.Interfaces;

namespace Stroytorg.Application.Features.Materials.CreateMaterial;

public record CreateMaterialCommand(
    string Name,
    string Description,
    int CategoryId,
    decimal Price,
    decimal StockAmount,
    decimal? Height,
    decimal? Width,
    decimal? Length,
    decimal? Weight)
    : ICommand<int>;
