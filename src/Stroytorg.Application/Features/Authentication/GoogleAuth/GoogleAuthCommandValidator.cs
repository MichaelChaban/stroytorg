using FluentValidation;
using Google.Apis.Auth;
using Stroytorg.Application.Constants;

namespace Stroytorg.Application.Features.Authentication.GoogleAuth;

internal class GoogleAuthCommandValidator : AbstractValidator<GoogleAuthCommand>
{
    public GoogleAuthCommandValidator()
    {
        RuleFor(user => user.Email)
            .Equal(user => new System.Net.Mail.MailAddress(user.Email).Address)
            .WithErrorCode(nameof(GoogleAuthCommand.Email))
            .WithMessage(BusinessErrorMessage.InvalidEmailFormat);

        RuleFor(user => user.Token)
            .MustAsync(ValidateGoogleUserAsync)
            .WithErrorCode(nameof(GoogleAuthCommand.Token))
            .WithMessage(BusinessErrorMessage.InvalidGoogleUserToken);
    }

    private static async Task<bool> ValidateGoogleUserAsync(string token, CancellationToken cancellationToken)
    {
        try
        {
            await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings());
            return true;
        }
        catch
        {
            return false;
        }
    }
}
