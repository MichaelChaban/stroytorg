using Stroytorg.Contracts.Models.Common;

namespace Stroytorg.Contracts.Models.User;

public record User : Auditable
{
    public int Id { get; init; }
    public string Email { get; init; }
    public string? Password { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? PhoneNumber { get; init; }
    public int Profile { get; init; }
    public string ProfileName { get; init; }
    public int AuthenticationType { get; init; }
    public string AuthenticationTypeName { get; init; }
    public DateTimeOffset BirthDate { get; init; }
    public ICollection<Order.Order> Orders { get; init; }

    public User(
        int id,
        string email,
        string? password,
        string firstName,
        string lastName,
        string? phoneNumber,
        int profile,
        string profileName,
        int authenticationType,
        string authenticationTypeName,
        DateTimeOffset birthDate,
        ICollection<Order.Order> orders,
        DateTimeOffset createdAt,
        string createdBy,
        DateTimeOffset? updatedAt,
        string updatedBy,
        DateTimeOffset? deactivatedAt,
        string deactivatedBy)
        : base(createdAt, createdBy, updatedAt, updatedBy, deactivatedAt, deactivatedBy)
    {
        (Id, Email, Password, FirstName, LastName, PhoneNumber, Profile, ProfileName, AuthenticationType, AuthenticationTypeName,
         BirthDate, Orders) = (id, email, password, firstName, lastName, phoneNumber, profile, profileName, authenticationType,
                             authenticationTypeName, birthDate, orders);
    }
}