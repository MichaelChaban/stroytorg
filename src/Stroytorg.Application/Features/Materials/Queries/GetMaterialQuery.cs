using MediatR;
using Stroytorg.Contracts.Models.Material;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Materials.Queries;

public record GetMaterialQuery(
    int MaterialId) : IRequest<BusinessResponse<MaterialDetail>>;