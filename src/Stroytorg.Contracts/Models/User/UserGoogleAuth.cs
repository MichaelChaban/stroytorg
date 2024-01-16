using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Contracts.Models.User;

public record UserGoogleAuth(
    [Required]
    string Token,

    [Required]
    string GoogleId,

    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [MaxLength(50)]
    string FirstName,

    [Required]
    [MaxLength(50)]
    string LastName);