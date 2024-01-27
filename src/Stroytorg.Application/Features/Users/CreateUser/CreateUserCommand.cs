using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Contracts.Models.User;

namespace Stroytorg.Application.Features.Users.CreateUser;

public record CreateUserCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    DateTimeOffset BirthDate,
    string PhoneNumber)
    : ICommand<User>;
