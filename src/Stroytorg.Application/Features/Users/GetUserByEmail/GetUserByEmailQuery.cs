using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Contracts.Models.User;

namespace Stroytorg.Application.Features.Users.GetUserByEmail;

public record GetUserByEmailQuery(
    string Email)
    : IQuery<User>;
