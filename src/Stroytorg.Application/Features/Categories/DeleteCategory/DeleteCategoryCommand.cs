using Stroytorg.Application.Abstractions.Interfaces;

namespace Stroytorg.Application.Features.Categories.DeleteCategory;

public record DeleteCategoryCommand(
    int CategoryId)
    : ICommand<int>;
