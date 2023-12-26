using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Domain.Data.Entities;

public class User : Auditable
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string PhoneNumber { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }

    public UserProfileEnum Profile { get; set; }

    public virtual ICollection<Order>? Orders { get; set; }
}
