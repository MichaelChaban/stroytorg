using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Materials.Commands;

public record DeleteMaterialCommand(
    int MaterialId
    ) : IRequest<BusinessResponse<int>>;
