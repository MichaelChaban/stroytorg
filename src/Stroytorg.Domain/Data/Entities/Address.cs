using Stroytorg.Domain.Data.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Domain.Data.Entities;

public class Address : BaseEntity
{
    public int UserId { get; set; }

    [Required]
    public required string Street { get; set; }

    [Required]
    public required string City { get; set; }

    [Required]
    public required string State { get; set; }

    [Required]
    public required string ZipCode { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<Order>? Orders { get; set; } 
}
