using Stroytorg.Application.Abstractions.Interfaces;

namespace Stroytorg.Application.Features.Categories.CreateCategory;

public record CreateCategoryCommand(
    string Name)
    : ICommand<int>;