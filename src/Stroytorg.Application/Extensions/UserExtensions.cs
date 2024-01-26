using Stroytorg.Application.Constants;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Domain.Data.Enums;
using Stroytorg.Infrastructure.Validations.Common;
using Contract = Stroytorg.Contracts.Enums;

namespace Stroytorg.Application.Extensions;

public static class UserExtensions
{
    public static bool ValidateUserAuthType(this int userAuthenticationType, Contract.AuthenticationType authenticationType, out BusinessResult<User>? businessResult)
    {
        businessResult = (userAuthenticationType, (int)authenticationType) switch
        {
            ((int)AuthenticationType.Internal, (int)AuthenticationType.Google) => BusinessResult.Failure<User>(new Error(nameof(BusinessErrorMessage.InternalAuthTypeError), BusinessErrorMessage.InternalAuthTypeError)),
            ((int)AuthenticationType.Google, (int)AuthenticationType.Internal) => BusinessResult.Failure<User>(new Error(nameof(BusinessErrorMessage.GoogleAuthTypeError), BusinessErrorMessage.GoogleAuthTypeError)),
            _ => null
        };

        return businessResult is null;
    }
}
