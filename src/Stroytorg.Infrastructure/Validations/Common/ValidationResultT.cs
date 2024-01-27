using Stroytorg.Infrastructure.Validations.Interfaces;

namespace Stroytorg.Infrastructure.Validations.Common;

public class ValidationResult<TValue>(Error[] errors) 
    : BusinessResult<TValue>(false, IValidationResult.ValidationError, default), IValidationResult
{
    public Error[] Errors { get; } = errors;

    public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
}
