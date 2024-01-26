using Stroytorg.Infrastructure.Validations.Interfaces;

namespace Stroytorg.Infrastructure.Validations.Common;

public class ValidationResult<TValue> : BusinessResult<TValue>, IValidationResult
{
    public ValidationResult(Error[] errors)
    : base(false, IValidationResult.ValidationError, default)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }

    public static ValidationResult<TValue> WithError(Error[] errors) => new(errors);
}
