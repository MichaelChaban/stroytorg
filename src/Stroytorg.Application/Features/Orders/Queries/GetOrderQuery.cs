using MediatR;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Orders.Queries;

public record GetOrderQuery(
    int OrderId) : IRequest<BusinessResponse<OrderDetail>>;