using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Domain.Data.Entities.Common;

public class Auditable : BaseEntity<int>
{
    [Required]
    [StringLength(255)]
    public required string CreatedBy { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    [StringLength(255)]
    public string? UpdatedBy { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    [StringLength(255)]
    public string? DeletedBy { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
