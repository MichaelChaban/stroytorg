namespace Stroytorg.Contracts.Models.User;

public record User(
    string Password,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime? BirthDate,
    int? Profile);
