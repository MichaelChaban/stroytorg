using PhoneNumbers;
using Stroytorg.Application.Constants;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Extensions;

public static class UserExtensions
{

    public static bool ValidateUser(this User user, out BusinessResponse<User>? businessResponse)
    {
        var phoneUtil = PhoneNumberUtil.GetInstance();
        var utilPhoneNumber = phoneUtil.Parse(user.PhoneNumber, null);
        if (!phoneUtil.IsValidNumber(utilPhoneNumber))
        {
            businessResponse = new BusinessResponse<User>(
                BusinessErrorMessage: BusinessErrorMessage.InvalidPhoneNumber,
                isSuccess: false);
            return false;
        }

        var mailAddress = new System.Net.Mail.MailAddress(user.Email).Address;
        if (!string.Equals(mailAddress, user.Email))
        {
            businessResponse = new BusinessResponse<User>(
                BusinessErrorMessage: BusinessErrorMessage.InvalidPhoneNumber,
                isSuccess: false);
            return false;
        }

        businessResponse = null;
        return true;
    }
}
