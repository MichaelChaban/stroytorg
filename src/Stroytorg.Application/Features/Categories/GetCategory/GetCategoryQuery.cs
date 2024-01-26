using MediatR;
using Stroytorg.Contracts.Models.Category;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Categories.GetCategory;

public record GetCategoryQuery(
    int CategoryId)
    : IRequest<BusinessResult<CategoryDetail>>;