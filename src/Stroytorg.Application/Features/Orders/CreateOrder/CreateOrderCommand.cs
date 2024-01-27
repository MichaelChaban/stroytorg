using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Contracts.Enums;
using Stroytorg.Contracts.Models.Order;

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
    : ICommand<int>;