using MediatR;

namespace Stroytorg.Application.Features.Orders.AssignOrderToUser;

public record AssignOrderToUserCommand(
    string UserEmail,
    int UserId)
    : IRequest;