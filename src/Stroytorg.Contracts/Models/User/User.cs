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
        DateTimeOffset BirthDate,
        ICollection<Order.Order> Orders,
        DateTimeOffset CreatedAt,
        string CreatedBy,
        DateTimeOffset? UpdatedAt,
        string UpdatedBy,
        DateTimeOffset? DeactivatedAt,
        string DeactivatedBy)
        : base(CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, DeactivatedAt, DeactivatedBy)
    {
        this.Id = Id;
        this.Email = Email;
        this.Password = Password;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.PhoneNumber = PhoneNumber;
        this.Profile = Profile;
        this.ProfileName = ProfileName;
        this.AuthenticationType = AuthenticationType;
        this.AuthenticationTypeName = AuthenticationTypeName;
        this.BirthDate = BirthDate;
        this.Orders = Orders;
    }
}