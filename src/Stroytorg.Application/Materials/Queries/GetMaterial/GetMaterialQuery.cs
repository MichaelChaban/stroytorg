using MediatR;
using Stroytorg.Contracts.Models.Material;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Materials.Queries.GetMaterial;

public record GetMaterialQuery(
        int MaterialId
    ) : IRequest<BusinessResponse<Material>>;
