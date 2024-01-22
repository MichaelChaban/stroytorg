using MediatR;
using Stroytorg.Contracts.Models.User;

namespace Stroytorg.Application.Features.Orders.Commands;

public record AssignOrderToUserCommand(
    User User) : IRequest;