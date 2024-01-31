using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Contracts.Enums;

namespace Stroytorg.Application.Features.Orders.UpdateOrder;

public record UpdateOrderCommand(
    int OrderId,
    OrderStatus OrderStatus)
    : ICommand<int>;
