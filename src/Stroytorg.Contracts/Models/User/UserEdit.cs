using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Contracts.Models.User;

public record UserEdit(
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]).{8,}$")]
    string? Password,

    [MaxLength(50)]
    string? FirstName,

    [MaxLength(50)]
    string? LastName,

    [MaxLength(50)]
    string? PhoneNumber,

    [Range(0, 100)]
    int? Profile);