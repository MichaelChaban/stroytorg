namespace Stroytorg.Contracts.Models.User;

public record User(
    int Id,
    string Email,
    string? Password,
    string FirstName,
    string LastName,
    string? PhoneNumber,
    int Profile,
    string ProfileName,
    int AuthenticationType,
    string AuthenticationTypeName,
    DateTimeOffset BirthDate);