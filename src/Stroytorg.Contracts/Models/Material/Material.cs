using Stroytorg.Contracts.Models.Common;

namespace Stroytorg.Contracts.Models.Material;

public record Material : Auditable
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int CategoryId { get; init; }
    public double Price { get; init; }
    public double StockAmount { get; init; }
    public bool IsFavorite { get; init; }
    public double? Height { get; init; }
    public double? Width { get; init; }
    public double? Length { get; init; }
    public double? Weight { get; init; }

    public Material(
        int Id,
        string Name,
        string Description,
        int CategoryId,
        double Price,
        double StockAmount,
        bool IsFavorite,
        double? Height,
        double? Width,
        double? Length,
        double? Weight,
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
        this.Description = Description;
        this.CategoryId = CategoryId;
        this.Price = Price;
        this.StockAmount = StockAmount;
        this.IsFavorite = IsFavorite;
        this.Height = Height;
        this.Width = Width;
        this.Length = Length;
        this.Weight = Weight;
    }
}
