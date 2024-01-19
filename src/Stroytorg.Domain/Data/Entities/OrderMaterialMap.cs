using Stroytorg.Domain.Data.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Domain.Data.Entities;

public class OrderMaterialMap : BaseEntity<int>
{
    [Required]
    public int OrderId { get; set; }

    [Required]
    public int MaterialId { get; set; }

    public decimal TotalMaterialAmount { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? TotalMaterialWeight { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Material? Material { get; set; }
}
