using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Materials.Commands.UpdateMaterial;

public record UpdateMaterialCommand(
    int MaterialId,
    string Name,
    string Description,
    int CategoryId,
    double Price,
    double StockAmount,
    double? Height,
    double? Width,
    double? Length,
    double? Weight,
    bool? IsFavorite
    ) : IRequest<BusinessResponse<int>>;
