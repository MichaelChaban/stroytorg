using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class DateRangeControlAttribute : ValidationAttribute
{
    public int YearsRange { get; }

    public DateRangeControlAttribute(int yearsRange)
    {
        YearsRange = yearsRange;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTimeOffset date)
        {
            var minDate = DateTimeOffset.UtcNow.AddYears(-YearsRange);

            if (date < minDate || date > DateTimeOffset.UtcNow)
            {
                return new ValidationResult($"Date must be between {minDate.ToString("yyyy-MM-dd")} and {DateTimeOffset.UtcNow.ToString("yyyy-MM-dd")}");
            }
        }

        return ValidationResult.Success;
    }
}