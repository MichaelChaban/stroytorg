using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Domain.Data.Entities.Common;

public class Auditable : BaseEntity
{
    [Required]
    [StringLength(255)]
    public required string CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    [StringLength(255)]
    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [StringLength(255)]
    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }
}
