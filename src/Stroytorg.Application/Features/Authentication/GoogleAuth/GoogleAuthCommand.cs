using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Authentication.GoogleAuth;

public record GoogleAuthCommand(
    string Token,
    string GoogleId,
    string Email,
    string FirstName,
    string LastName)
    : IRequest<AuthResponse>;