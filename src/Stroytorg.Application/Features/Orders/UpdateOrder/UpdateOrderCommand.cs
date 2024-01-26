using MediatR;
using Stroytorg.Contracts.Enums;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Orders.UpdateOrder;

public record UpdateOrderCommand(
    int OrderId,
    OrderStatus OrderStatus)
    : IRequest<BusinessResult<int>>;
