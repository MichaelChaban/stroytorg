using MediatR;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Authentication.Queries;

public record LoginQuery(
    UserLogin User) : IRequest<AuthResponse>;