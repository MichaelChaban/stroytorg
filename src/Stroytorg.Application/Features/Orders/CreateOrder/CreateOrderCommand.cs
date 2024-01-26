using MediatR;
using Stroytorg.Contracts.Enums;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Orders.CreateOrder;

public record CreateOrderCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    ShippingType ShippingType,
    PaymentType PaymentType,
    IEnumerable<MaterialMapCreate> Materials,
    string? ShippingAddress)
    : IRequest<BusinessResult<int>>;