using MediatR;
using Stroytorg.Contracts.Models.Material;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Materials.Commands;

public record CreateMaterialCommand(
    MaterialEdit Material) : IRequest<BusinessResponse<int>>;
