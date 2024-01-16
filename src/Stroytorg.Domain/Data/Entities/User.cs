using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Data.Enums;
using Stroytorg.Domain.Extensions;
using Stroytorg.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Domain.Data.Entities;

public class User : Auditable
{
    private string? password;

    [Password]
    public string? Password
    {
        get => password;
        set => password = value is null ? null : value.HashAndSaltPassword();
    }

    [Required]
    [MaxLength(50)]
    public required string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public required string LastName { get; set; }

    [Required]
    [MaxLength(150)]
    [EmailAddress]
    public required string Email { get; set; }

    [PhoneNumber]
    [MaxLength(50)]
    public string? PhoneNumber { get; set; }

    public string? GoogleId { get; set; }

    [Required]
    [DateRangeControl(yearsRange: 100)]
    public DateTimeOffset BirthDate { get; set; }

    public UserProfile Profile { get; set; } = UserProfile.Customer;

    public AuthenticationType AuthenticationType { get; set; } = AuthenticationType.Internal;

    public virtual ICollection<Order>? Orders { get; set; }
}
