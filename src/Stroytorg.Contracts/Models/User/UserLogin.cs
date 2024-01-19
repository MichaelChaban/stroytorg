using Stroytorg.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Contracts.Models.User;

public record UserLogin(
    [Required]
    [EmailAddress]
    [MaxLength(150)]
    string Email,

    [Required]
    string Password);
