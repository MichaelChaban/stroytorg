using System.ComponentModel.DataAnnotations;
using Stroytorg.Infrastructure.Attributes;

namespace Stroytorg.Contracts.Models.User;

public record UserRegister(
    [Required]
    [MaxLength(150)]
    string Email,

    [Required]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]).{8,}$",
        ErrorMessage = "Password didn't pass the needed requirements.")]
    string Password,

    [Required]
    [MaxLength(50)]
    string FirstName,

    [Required]
    [MaxLength(50)]
    string LastName,

    [DateRangeControl(yearsRange: 100)]
    DateTimeOffset BirthDate,

    [MaxLength(50)]
    string PhoneNumber);