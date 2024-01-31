using Stroytorg.Application.Abstractions.Interfaces;

namespace Stroytorg.Application.Features.Materials.DeleteMaterial;

public record DeleteMaterialCommand(
    int MaterialId)
    : ICommand<int>;