using Stroytorg.Contracts.Models.Common;

namespace Stroytorg.Contracts.Models.Material;

public record MaterialDetail : Auditable
{
    public string Name { get; init; }
    public string Description { get; init; }
    public int CategoryId { get; init; }
    public decimal Price { get; init; }
    public decimal StockAmount { get; init; }
    public bool IsFavorite { get; init; }
    public decimal? Height { get; init; }
    public decimal? Width { get; init; }
    public decimal? Length { get; init; }
    public decimal? Weight { get; init; }

    public MaterialDetail(
        string Name,
        string Description,
        int CategoryId,
        decimal Price,
        decimal StockAmount,
        bool IsFavorite,
        decimal? Height,
        decimal? Width,
        decimal? Length,
        decimal? Weight,
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
