using Stroytorg.Contracts.Models.Common;

namespace Stroytorg.Contracts.Models.Category;

public record CategoryDetail : Auditable
{
    public string Name { get; init; }
    public IEnumerable<Material.Material>? Materials { get; init; }

    public CategoryDetail(
        string Name,
        IEnumerable<Material.Material>? Materials,
        DateTimeOffset CreatedAt,
        string CreatedBy,
        DateTimeOffset? UpdatedAt,
        string UpdatedBy,
        DateTimeOffset? DeactivatedAt,
        string DeactivatedBy,
        int Id,
        bool IsActive)
        : base(CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, DeactivatedAt, DeactivatedBy, Id, IsActive)
    {
        this.Name = Name;
        this.Materials = Materials;
    }
}