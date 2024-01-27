using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Contracts.Models.Material;

namespace Stroytorg.Application.Features.Materials.GetMaterial;

public record GetMaterialQuery(
    int MaterialId)
    : IQuery<MaterialDetail>;