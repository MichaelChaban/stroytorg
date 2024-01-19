namespace Stroytorg.Contracts.Models.Order;

public record Order(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    decimal TotalPrice,
    int ShippingType,
    string ShippingTypeName,
    int PaymentType,
    string PaymentTypeName,
    int OrderStatus,
    string OrderStatusName,
    int? UserId,
    string? ShippingAddress);