using MediatR;
using Stroytorg.Contracts.Models.Category;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Categories.Commands;

public record CreateCategoryCommand(
    CategoryEdit Category) : IRequest<BusinessResponse<int>>;
