using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Authentication.Register;

public record RegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    DateTimeOffset BirthDate,
    string PhoneNumber)
    : ICommand<JwtTokenResponse>;