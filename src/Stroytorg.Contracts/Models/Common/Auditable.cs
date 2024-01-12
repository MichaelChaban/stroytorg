namespace Stroytorg.Contracts.Models.Common;

public record Auditable(
    DateTimeOffset CreatedAt,
    string CreatedBy,
    DateTimeOffset? UpdatedAt,
    string? UpdatedBy,
    DateTimeOffset? DeactivatedAt,
    string? DeactivatedBy);
