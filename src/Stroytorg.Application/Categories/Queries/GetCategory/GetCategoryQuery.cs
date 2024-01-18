using MediatR;
using Stroytorg.Contracts.Models.Category;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Categories.Queries.GetCategory;

public record GetCategoryQuery(
        int CategoryId
    ) : IRequest<BusinessResponse<Category>>;
