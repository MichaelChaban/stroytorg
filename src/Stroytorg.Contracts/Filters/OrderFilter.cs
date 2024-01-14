namespace Stroytorg.Contracts.Filters;

public record OrderFilter(
    string? Fullname,
    string? Email,
    string? PhoneNumber,
    int? MinMaterialsAmount,
    int? MaxMaterialsAmount,
    int? MinTotalPrice,
    int? MaxTotalPrice,
    int? ShippingType,
    int? PaymentType);