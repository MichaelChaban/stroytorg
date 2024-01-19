using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class EnumValidationAttribute : ValidationAttribute
{
    private readonly Type enumType;

    public EnumValidationAttribute(Type enumType)
    {
        this.enumType = enumType;
    }

    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }

        return Enum.IsDefined(enumType, value);
    }
}
