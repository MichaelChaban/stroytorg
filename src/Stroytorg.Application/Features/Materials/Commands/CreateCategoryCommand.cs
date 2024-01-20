using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Materials.Commands;

public record CreateMaterialCommand(
    string Name,
    string Description,
    int CategoryId,
    decimal Price,
    decimal StockAmount,
    decimal? Height,
    decimal? Width,
    decimal? Length,
    decimal? Weight) :
    IRequest<BusinessResponse<int>>;
