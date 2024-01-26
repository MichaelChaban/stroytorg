using MediatR;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Orders.GetOrder;

public record GetOrderQuery(
    int OrderId)
    : IRequest<BusinessResult<OrderDetail>>;