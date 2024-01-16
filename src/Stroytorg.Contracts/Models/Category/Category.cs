using Stroytorg.Contracts.Models.Common;

namespace Stroytorg.Contracts.Models.Category;

public record Category : Auditable
{
    public int Id { get; init; }
    public string Name { get; init; }
    public IEnumerable<Material.Material>? Materials { get; init; }

    public Category(
        int Id,
        string Name,
        IEnumerable<Material.Material>? Materials,
        DateTimeOffset CreatedAt,
        string CreatedBy,
        DateTimeOffset? UpdatedAt,
        string UpdatedBy,
        DateTimeOffset? DeactivatedAt,
        string DeactivatedBy)
        : base(CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, DeactivatedAt, DeactivatedBy)
    {
        this.Id = Id;
        this.Name = Name;
        this.Materials = Materials;
    }
}