using Stroytorg.Domain.Data.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Domain.Data.Entities;

public class OrderMaterialMap : BaseEntity
{
    [Required]
    public int OrderId { get; set; }

    [Required]
    public int MaterialId{ get; set; }

    public double? TotalMaterialAmount { get; set; }

    public double? TotalMaterialWeight { get; set; }

    public double? UnitPrice { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Material? Material { get; set; }
}
