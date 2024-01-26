using MediatR;
using Stroytorg.Contracts.Models.Material;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Materials.GetMaterial;

public record GetMaterialQuery(
    int MaterialId)
    : IRequest<BusinessResult<MaterialDetail>>;