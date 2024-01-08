using Google.Apis.Auth;
using Stroytorg.Application.Constants;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Enums;
using Contract = Stroytorg.Contracts.Enums;

namespace Stroytorg.Application.Extensions;

public static class UserExtensions
{

    public static bool ValidateUser(this User user, out BusinessResponse<User>? businessResponse)
    {
        try
        {
            if (user.AuthenticationType.ValidateUserAuthType(Contract.AuthenticationType.Internal, out var businessError))
            {
                businessResponse = new BusinessResponse<User>(BusinessErrorMessage: businessError!.BusinessErrorMessage);
                return false;
            }

            businessResponse = null;
            return true;
        }
        catch
        {
            businessResponse = new BusinessResponse<User>(
                BusinessErrorMessage: BusinessErrorMessage.UserValidationError,
                isSuccess: false);
            return false;
        }
    }

    public static bool ValidateUserAuthType(this int userAuthenticationType, Contract.AuthenticationType authenticationType, out BusinessResponse<User>? businessResponse)
    {
        businessResponse = (userAuthenticationType, (int)authenticationType) switch
        {
            ((int)AuthenticationType.Internal, (int)AuthenticationType.Google) => new BusinessResponse<User> { BusinessErrorMessage = BusinessErrorMessage.InternalAuthType },
            ((int)AuthenticationType.Google, (int)AuthenticationType.Internal) => new BusinessResponse<User> { BusinessErrorMessage = BusinessErrorMessage.GoogleAuthType },
            _ => null
        };

        return businessResponse is null;
    }

    public static async Task<BusinessResponse<User>?> ValidateGoogleUserAsync(this UserGoogleAuth user)
    {
        try
        {
            await GoogleJsonWebSignature.ValidateAsync(user.Token, new GoogleJsonWebSignature.ValidationSettings());

            var mailAddress = new System.Net.Mail.MailAddress(user.Email).Address;
            if (!string.Equals(mailAddress, user.Email))
            {
                return new BusinessResponse<User>(
                    BusinessErrorMessage: BusinessErrorMessage.InvalidPhoneNumber,
                    isSuccess: false);
            }

            return null;
        }
        catch
        {
            return new BusinessResponse<User>(
                BusinessErrorMessage: BusinessErrorMessage.UserValidationError,
                isSuccess: false);
        }
    }
}
