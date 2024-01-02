namespace Stroytorg.Contracts.Models.Order;

public record Order(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    int MaterialsAmount,
    double TotalPrice,
    int ShippingType,
    string ShippingTypeName,
    int PaymentType,
    string PaymentTypeName,
    int OrderStatus,
    string OrderStatusName,
    int? UserId,
    string? ShippingAddress);
