using MediatR;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Materials.DeleteMaterial;

public record DeleteMaterialCommand(
    int MaterialId)
    : IRequest<BusinessResult<int>>;