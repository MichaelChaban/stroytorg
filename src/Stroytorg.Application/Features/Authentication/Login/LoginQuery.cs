using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Authentication.Login;

public record LoginQuery(
    string Email,
    string Password)
    : IRequest<AuthResponse>;