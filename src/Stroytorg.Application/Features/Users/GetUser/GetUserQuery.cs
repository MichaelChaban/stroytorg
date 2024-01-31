using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Contracts.Models.User;

namespace Stroytorg.Application.Features.Users.GetUser;

public record GetUserQuery(
    int UserId)
    : IQuery<UserDetail>;
