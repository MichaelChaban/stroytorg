using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
    ) : IRequest<AuthResponse>;