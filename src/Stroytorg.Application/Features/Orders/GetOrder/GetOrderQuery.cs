using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Contracts.Models.Order;

namespace Stroytorg.Application.Features.Orders.GetOrder;

public record GetOrderQuery(
    int OrderId)
    : IQuery<OrderDetail>;