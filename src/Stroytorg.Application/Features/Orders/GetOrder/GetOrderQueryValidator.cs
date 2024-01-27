using FluentValidation;
using Stroytorg.Application.Constants;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Orders.GetOrder;

internal class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
{
    private readonly IOrderRepository orderRepository;
    public GetOrderQueryValidator(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));

        RuleFor(order => order.OrderId)
            .MustAsync(OrderWithIdExistsAsync)
            .WithErrorCode(nameof(GetOrderQuery.OrderId))
            .WithMessage(BusinessErrorMessage.NotExistingOrderWithId);
    }

    private async Task<bool> OrderWithIdExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await orderRepository.ExistsAsync(id, cancellationToken);
    }
}
