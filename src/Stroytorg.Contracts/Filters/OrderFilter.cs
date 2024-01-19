namespace Stroytorg.Contracts.Filters;

public record OrderFilter(
    string? Fullname,
    string? Email,
    string? PhoneNumber,
    double? MinTotalPrice,
    double? MaxTotalPrice,
    int? ShippingType,
    int? PaymentType);