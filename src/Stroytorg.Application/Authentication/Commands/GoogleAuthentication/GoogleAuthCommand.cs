using MediatR;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Authentication.Commands.GoogleAuthentication;

public record GoogleAuthCommand(
    string Token,
    string GoogleId,
    string Email,
    string FirstName,
    string LastName
    ): IRequest<AuthResponse>;
