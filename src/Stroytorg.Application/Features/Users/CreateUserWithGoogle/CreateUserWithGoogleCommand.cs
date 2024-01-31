using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Contracts.Models.User;

namespace Stroytorg.Application.Features.Users.CreateUserWithGoogle;

public record CreateUserWithGoogleCommand(
    string Token,
    string GoogleId,
    string Email,
    string FirstName,
    string LastName)
    : ICommand<User>;