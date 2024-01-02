using Stroytorg.Domain.Data.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Domain.Data.Entities;

public class Category : Auditable
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    public virtual ICollection<Material>? Materials { get; set; }
}
