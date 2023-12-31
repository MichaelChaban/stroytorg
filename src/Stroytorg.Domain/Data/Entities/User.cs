using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Enums;
using Stroytorg.Domain.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Domain.Data.Entities;

public class User : Auditable
{
    private string password;

    public required string Password
    {
        get
        {
            return password;
        }
        set
        {
            password = value.HashAndSaltPassword();
        }
    }

    [Required]
    [MaxLength(50)]
    public required string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public required string LastName { get; set; }

    [MaxLength(150)]
    public required string Email { get; set; }

    [Required]
    [MaxLength(50)]
    public string? PhoneNumber { get; set; }

    public DateTime BirthDate { get; set; }

    public UserProfileEnum Profile { get; set; }

    public virtual ICollection<Order>? Orders { get; set; }
}
