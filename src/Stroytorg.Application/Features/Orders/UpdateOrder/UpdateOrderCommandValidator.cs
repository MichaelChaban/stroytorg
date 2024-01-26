using FluentValidation;
using Stroytorg.Application.Constants;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Orders.UpdateOrder;

internal class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    private readonly IOrderRepository orderRepository;

    public UpdateOrderCommandValidator(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));

        RuleFor(order => order.OrderId)
            .MustAsync(OrderWithIdExistsAsync)
            .WithMessage(BusinessErrorMessage.NotExistingOrderWithId);

        RuleFor(order => order.OrderId)
            .MustAsync(OrderIsActiveAsync)
            .WithMessage(BusinessErrorMessage.NotActiveOrderWithId);
    }

    private async Task<bool> OrderWithIdExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await orderRepository.ExistsAsync(id, cancellationToken);
    }

    private async Task<bool> OrderIsActiveAsync(int id, CancellationToken cancellationToken)
    {
        return (await orderRepository.GetAsync(id, cancellationToken)).IsActive;
    }
}
