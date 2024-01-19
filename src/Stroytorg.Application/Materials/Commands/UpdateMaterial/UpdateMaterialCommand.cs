using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Materials.Commands.UpdateMaterial;

public record UpdateMaterialCommand(
    int MaterialId,
    string Name,
    string Description,
    int CategoryId,
    decimal Price,
    decimal StockAmount,
    decimal? Height,
    decimal? Width,
    decimal? Length,
    decimal? Weight,
    bool? IsFavorite
    ) : IRequest<BusinessResponse<int>>;
