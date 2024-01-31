using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Infrastructure.Validations.Common;
using Stroytorg.Infrastructure.Validations.Interfaces;

namespace Stroytorg.Host.Abstractions;

[ApiController]
public abstract class ApiController(ISender mediatR) : ControllerBase
{
    protected readonly ISender mediatR = mediatR ?? throw new ArgumentNullException(nameof(mediatR));

    protected IActionResult HandleResult<T>(BusinessResult result)
    {
        return result switch
        {
            { IsSuccess: false } => HandleFailure<T>(result),
            { IsSuccess: true } => HandleSuccess<T>(result),
            _ => throw new InvalidOperationException()
        };
    }

    protected IActionResult HandleFailure<T>(BusinessResult result)
    {
        return result switch
        {
            IValidationResult validationResult => BadRequest(CreateProblemDetails(
                        "Validation Error",
                        StatusCodes.Status400BadRequest,
                        result.Error!,
                        validationResult.Errors)),
            BusinessResult<JwtTokenResponse> authResult => Unauthorized(authResult.Error),
            _ => BadRequest(CreateProblemDetails(
                        "Bad Request",
                        StatusCodes.Status400BadRequest,
                        result.Error!))
        };
    }

    protected IActionResult HandleSuccess<T>(BusinessResult result)
    {
        return result switch
        {
            BusinessResult<T> successResult => Ok(successResult.Value),
            _ => Ok()
        };
    }

    private static ProblemDetails CreateProblemDetails(string title, int status, Error error, Error[]? errors = null)
    {
        return new()
            {
                Title = title,
                Type = error.Code,
                Detail = error.Message,
                Status = status,
                Extensions = { { nameof(errors), errors } }
            };
    }
}
