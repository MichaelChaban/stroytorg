using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Materials.Commands.DeleteMaterial;

public record DeleteMaterialCommand(
    int MaterialId
    ) : IRequest<BusinessResponse<int>>;
