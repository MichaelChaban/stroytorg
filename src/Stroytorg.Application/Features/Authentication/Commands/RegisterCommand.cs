using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Authentication.Commands;

public record RegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    DateTimeOffset BirthDate,
    string PhoneNumber) : IRequest<AuthResponse>;
