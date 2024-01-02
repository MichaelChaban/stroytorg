namespace Stroytorg.Application.Constants;

public static class BusinessErrorMessage
{
    public static string NotExistingUser { get; } = "There is no user was found with the provided credentials.";

    public static string NotCreatedUser { get; } = "User was not created with the provided credentials.";

    public static string AlreadyExistingUser { get; } = "There is an already existing user with the provided credentials.";

    public static string IncorrectPassword { get; } = "Provided password is not correct.";

    public static string InvalidPhoneNumber { get; } = "Provided phone number is not valid.";

    public static string InvalidEmail { get; } = "Provided email address is not valid.";
}
