namespace Stroytorg.Contracts.Models.Order;

public record MaterialMap : Material.Material
{
    public int OrderId { get; init; }
    public double TotalMaterialAmount { get; init; }
    public double UnitPrice { get; init; }
    public MaterialMap(
        int OrderId,
        double TotalMaterialAmount,
        double UnitPrice,
        Material.Material Material) : base(Material)
    {
        this.OrderId = OrderId;
        this.TotalMaterialAmount = TotalMaterialAmount;
        this.UnitPrice = UnitPrice;
    }
}