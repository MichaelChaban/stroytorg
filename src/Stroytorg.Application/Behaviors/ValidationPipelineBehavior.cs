using FluentValidation;
using MediatR;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : BusinessResult
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) =>
        this.validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var errors = (await Task.WhenAll(validators
            .Select(async validator =>
            {
                var validationResult = await validator.ValidateAsync(request);
                return validationResult.Errors
                    .Where(validationFailure => validationFailure is not null)
                    .Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage));
            })))
            .SelectMany(validationFailures => validationFailures)
            .Distinct()
            .ToArray();


        if (errors.Any())
        {
            return CreateValidationResult<TResponse>(errors);
        }

        return await next();
    }

    private static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : BusinessResult
    {
        var tResultType = typeof(TResult);
        var businessReult = typeof(BusinessResult);

        if (typeof(TResult) == typeof(BusinessResult))
        {
            return (ValidationResult.WithErrors(errors) as TResult)!;
        }

        object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object?[] { errors })!;

        return (TResult)validationResult;
    }
}
