using MediatR;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Orders.Commands;

public record UpdateOrderCommand(
    int OrderId,
    OrderEdit Order) : IRequest<BusinessResponse<int>>;
