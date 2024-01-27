using Stroytorg.Application.Abstractions.Interfaces;

namespace Stroytorg.Application.Features.Categories.UpdateCategory;

public record UpdateCategoryCommand(
    int CategoryId,
    string Name)
    : ICommand<int>;