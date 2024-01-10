using Stroytorg.Contracts.Models.Common;

namespace Stroytorg.Contracts.Models.Order;

public record Order : Auditable
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public int MaterialsAmount { get; init; }
    public double TotalPrice { get; init; }
    public int ShippingType { get; init; }
    public string ShippingTypeName { get; init; }
    public int PaymentType { get; init; }
    public string PaymentTypeName { get; init; }
    public int OrderStatus { get; init; }
    public string OrderStatusName { get; init; }
    public int? UserId { get; init; }
    public string? ShippingAddress { get; init; }

    public Order(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        int materialsAmount,
        double totalPrice,
        int shippingType,
        string shippingTypeName,
        int paymentType,
        string paymentTypeName,
        int orderStatus,
        string orderStatusName,
        int? userId,
        string? shippingAddress,
        DateTimeOffset createdAt,
        string createdBy,
        DateTimeOffset? updatedAt,
        string updatedBy,
        DateTimeOffset? deactivatedAt,
        string deactivatedBy)
        : base(createdAt, createdBy, updatedAt, updatedBy, deactivatedAt, deactivatedBy)
    {
        (FirstName, LastName, Email, PhoneNumber, MaterialsAmount, TotalPrice, ShippingType, ShippingTypeName, PaymentType,
         PaymentTypeName, OrderStatus, OrderStatusName, UserId, ShippingAddress) = (firstName, lastName, email, phoneNumber,
                                                                                    materialsAmount, totalPrice, shippingType,
                                                                                    shippingTypeName, paymentType, paymentTypeName,
                                                                                    orderStatus, orderStatusName, userId, shippingAddress);
    }
}
