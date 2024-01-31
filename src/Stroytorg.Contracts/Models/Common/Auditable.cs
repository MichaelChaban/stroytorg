namespace Stroytorg.Contracts.Models.Common;

public record Auditable : BaseRecord
{
    public DateTimeOffset CreatedAt { get; init; }
    public string CreatedBy { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
    public string? UpdatedBy { get; init; }
    public DateTimeOffset? DeactivatedAt { get; init; }
    public string? DeactivatedBy { get; init; }

    public Auditable(
        DateTimeOffset CreatedAt,
        string CreatedBy,
        DateTimeOffset? UpdatedAt,
        string? UpdatedBy,
        DateTimeOffset? DeactivatedAt,
        string? DeactivatedBy,
        int Id,
        bool IsActive)
        : base(Id, IsActive)
    {
        this.CreatedAt = CreatedAt;
        this.CreatedBy = CreatedBy;
        this.UpdatedAt = UpdatedAt;
        this.UpdatedBy = UpdatedBy;
        this.DeactivatedAt = DeactivatedAt;
        this.DeactivatedBy = DeactivatedBy;
    }
}
