using MediatR;
using Stroytorg.Contracts.Models.Category;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Categories.Queries;

public record GetCategoryQuery(
    int CategoryId) : IRequest<BusinessResponse<CategoryDetail>>;