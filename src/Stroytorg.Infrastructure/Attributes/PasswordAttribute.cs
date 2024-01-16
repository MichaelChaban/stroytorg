using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Stroytorg.Infrastructure.Attributes;

public class PasswordAttribute : ValidationAttribute
{
    private readonly string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]).{8,}$";

    public override bool IsValid(object? value)
    {
        if (value is null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return true;
        }

        var password = value.ToString();
        return Regex.IsMatch(password!, PasswordRegex);
    }
}
