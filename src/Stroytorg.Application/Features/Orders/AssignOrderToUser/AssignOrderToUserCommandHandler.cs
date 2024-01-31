using MediatR;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Orders.AssignOrderToUser;

public class AssignOrderToUserCommandHandler(
    IOrderRepository orderRepository) :
    IRequestHandler<AssignOrderToUserCommand>
{
    private readonly IOrderRepository orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));

    public async Task Handle(AssignOrderToUserCommand command, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetOrdersByEmailAsync(command.UserEmail);

        foreach (var order in orders)
        {
            order.UserId = command.UserId;
        }

        orderRepository.UpdateRange(orders);
        await orderRepository.UnitOfWork.CommitAsync();
    }
}
