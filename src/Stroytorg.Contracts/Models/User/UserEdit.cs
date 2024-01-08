using Stroytorg.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Contracts.Models.User;

public record UserEdit(
    [Password(ErrorMessage = "Password didn't pass the needed requirements.")]
    string? Password,

    [MaxLength(50)]
    string? FirstName,

    [MaxLength(50)]
    string? LastName,

    [MaxLength(50)]
    [PhoneNumber]
    string? PhoneNumber,

    [Range(0, 100)]
    int? Profile);