using MediatR;
using Stroytorg.Application.Features.Orders.Commands;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Orders.CommandHandlers;

public class AssignOrderToUserCommandHandler(
    IOrderRepository orderRepository) :
    IRequestHandler<AssignOrderToUserCommand>
{
    private readonly IOrderRepository orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));

    public async Task Handle(AssignOrderToUserCommand request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetOrdersByEmailAsync(request.User.Email);
        foreach (var order in orders)
        {
            order.UserId = request.User.Id;
        }
        orderRepository.UpdateRange(orders);
        await orderRepository.UnitOfWork.CommitAsync();
    }
}
