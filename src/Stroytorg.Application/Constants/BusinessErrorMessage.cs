namespace Stroytorg.Application.Constants;

public static class BusinessErrorMessage
{
    public static string NotExistingUser { get; } = "There is no user was found with the provided credentials.";

    public static string AlreadyExistingUser { get; } = "There is an already existing user with the provided credentials.";

    public static string IncorrectPassword { get; } = "Provided password is not correct.";

    public static string InvalidPhoneNumber { get; } = "Provided phone number is not valid.";

    public static string UserValidationError { get; } = "There is an error occurred while validating the user.";

    public static string GoogleAuthType { get; } = "User was authenticated with Google.";

    public static string InternalAuthType { get; } = "User was authenticated with an email and a password.";

    public static string NotExistingEntity { get; } = "There is no entity was found with the provided id.";

    public static string AlreadyExistingEntity { get; } = "There is an already existing in the db.";

    public static string OperationCancelled { get; } = "Operation was cancelled.";

    public static string UnableToDeleteEntity { get; } = "Unable to Delete Entity";

}
