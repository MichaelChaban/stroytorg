using MediatR;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Orders.Commands;

public record CreateOrderCommand(
    OrderCreate Order) : IRequest<BusinessResponse<int>>;