using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Infrastructure.Validations.Interfaces;

public interface IValidationResult
{
    static readonly Error ValidationError = new Error("ValidationError", "A validation problem occurred.");

    Error[] Errors { get; }
}
