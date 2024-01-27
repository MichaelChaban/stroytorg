using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Contracts.Models.Category;

namespace Stroytorg.Application.Features.Categories.GetCategory;

public record GetCategoryQuery(
    int CategoryId)
    : IQuery<CategoryDetail>;