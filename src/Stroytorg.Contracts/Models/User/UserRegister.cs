using System.ComponentModel.DataAnnotations;
using Stroytorg.Infrastructure.Attributes;

namespace Stroytorg.Contracts.Models.User;

public record UserRegister(
    [Required]
    [EmailAddress]
    [MaxLength(150)]
    string Email,

    [Required]
    [Password(ErrorMessage = "Password didn't pass the needed requirements.")]
    string Password,

    [Required]
    [MaxLength(50)]
    string FirstName,

    [Required]
    [MaxLength(50)]
    string LastName,

    [Required]
    [DateRangeControl(yearsRange: 100)]
    DateTimeOffset BirthDate,

    [Required]
    [MaxLength(50)]
    [PhoneNumber]
    string PhoneNumber);