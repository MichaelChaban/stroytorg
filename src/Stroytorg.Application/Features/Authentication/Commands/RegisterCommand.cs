using MediatR;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Authentication.Commands;

public record RegisterCommand(
    UserRegister User) : IRequest<AuthResponse>;