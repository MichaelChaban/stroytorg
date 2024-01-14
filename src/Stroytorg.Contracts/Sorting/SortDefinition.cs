using Stroytorg.Contracts.Enums;

namespace Stroytorg.Contracts.Sorting;

public record SortDefinition(
    string? Field,
    SortDirection Direction = SortDirection.Ascending);