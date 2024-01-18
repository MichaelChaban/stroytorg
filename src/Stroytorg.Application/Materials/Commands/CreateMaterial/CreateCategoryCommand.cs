using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Materials.Commands.CreateMaterial;

public record CreateMaterialCommand(
    string Name,
    string Description,
    int CategoryId,
    double Price,
    double StockAmount,
    double? Height,
    double? Width,
    double? Length,
    double? Weight
    ) : IRequest<BusinessResponse<int>>;
