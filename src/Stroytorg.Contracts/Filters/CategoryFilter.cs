namespace Stroytorg.Contracts.Filters;

public record CategoryFilter(
    int? Id,
    string? Name,
    bool? IsActive);