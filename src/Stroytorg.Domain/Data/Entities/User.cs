using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Enums;
using Stroytorg.Domain.Extensions;
using Stroytorg.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Domain.Data.Entities;

public class User : Auditable
{
    private string password;

    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]).{8,}$")]
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

    [Required]
    [DateRangeControl(yearsRange: 100)]
    public DateTimeOffset BirthDate { get; set; }

    public UserProfile Profile { get; set; } = UserProfile.Customer;

    public virtual ICollection<Order>? Orders { get; set; }
}
