using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Contracts.Models.User;

public record UserLogin(
    [Required]
    [MaxLength(150)]
    string Email,

    [Required]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]).{8,}$",
        ErrorMessage = "Password didn't pass the needed requirements.")]
    string Password);
