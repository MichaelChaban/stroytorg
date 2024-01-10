using Stroytorg.Contracts.Models.Common;

namespace Stroytorg.Contracts.Models.Category;

public record Category : Auditable
{
    public int Id { get; init; }

    public string Name { get; init; }

    public IEnumerable<Material.Material>? Materials { get; init; }

    public Category(int id, string name, DateTimeOffset createdAt, string createdBy, DateTimeOffset? updatedAt, string updatedBy, DateTimeOffset? deactivatedAt, string deactivatedBy, IEnumerable<Material.Material>? materials)
        : base(createdAt, createdBy, updatedAt, updatedBy, deactivatedAt, deactivatedBy)
        => (Id, Name, Materials ) = (id, name, materials);
}