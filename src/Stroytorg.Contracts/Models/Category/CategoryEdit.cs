using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Contracts.Models.Category;

public record CategoryEdit(
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    string Name);