using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Contracts.Models.User;

public record UserLogin(
    [Required]
    string Email,

    [Required]
    string Password);
