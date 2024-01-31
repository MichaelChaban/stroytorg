using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Authentication.Login;

public record LoginQuery(
    string Email,
    string Password)
    : IQuery<JwtTokenResponse>;