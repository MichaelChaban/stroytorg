using MediatR;
using Stroytorg.Contracts.Models.Category;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Categories.Commands;

public record UpdateCategoryCommand(
    int CategoryId,
    CategoryEdit Category) : IRequest<BusinessResponse<int>>;
