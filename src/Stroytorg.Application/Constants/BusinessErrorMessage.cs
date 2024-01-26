namespace Stroytorg.Application.Constants;

public static class BusinessErrorMessage
{
    public static string GoogleAuthTypeError { get; } = "User was authenticated with Google.";

    public static string InternalAuthTypeError { get; } = "User was authenticated with an email and a password.";

    public static string InvalidGoogleUserToken { get; } = "Invalid google user token";

    public static string InvalidEmailFormat { get; } = "Invalid email format";

    public static string InvalidPassword { get; } = "Invalid password";

    public static string InvalidShippingInformation { get; } = "Invalid shipping information";

    public static string InvalidMaterialsQuantity { get; } = "Bigger quantity was provided to order";

    public static string ExistingUserWithEmail { get; } = "Already existing user with the provided email";

    public static string NotExistingUserWithEmail { get; } = "No user was found with the provided email";

    public static string NotExistingUserWithId { get; } = "No user was found with the provided id";

    public static string ExistingCategoryWithName { get; } = "Already existing category with the provided name";

    public static string NotExistingCategoryWithId { get; } = "No category was found with the provided id";

    public static string ExistingMaterialsWithCategoryId { get; } = "There are existing materials with the provided category id";

    public static string ExistingMaterialsWithName { get; } = "Already existing material with the provided name";

    public static string NotExistingMaterialWithId { get; } = "No material was found with the provided id";

    public static string NotExistingOrderWithId { get; } = "No order was found with the provided id";

    public static string NotActiveOrderWithId { get; } = "The order is already inactive";
}
