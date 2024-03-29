﻿using Stroytorg.Contracts.Models.Common;

namespace Stroytorg.Contracts.Models.Order;

public record OrderDetail : Auditable
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public decimal TotalPrice { get; init; }
    public int ShippingType { get; init; }
    public string ShippingTypeName { get; init; }
    public int PaymentType { get; init; }
    public string PaymentTypeName { get; init; }
    public int OrderStatus { get; init; }
    public string OrderStatusName { get; init; }
    public int? UserId { get; init; }
    public string? ShippingAddress { get; init; }
    public IEnumerable<MaterialMap>? Materials { get; init; }

    public OrderDetail(
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
        string? ShippingAddress,
        IEnumerable<MaterialMap>? Materials,
        DateTimeOffset CreatedAt,
        string CreatedBy,
        DateTimeOffset? UpdatedAt,
        string UpdatedBy,
        DateTimeOffset? DeactivatedAt,
        string DeactivatedBy,
        int Id,
        bool IsActive)
        : base(CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, DeactivatedAt, DeactivatedBy, Id, IsActive)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Email = Email;
        this.PhoneNumber = PhoneNumber;
        this.TotalPrice = TotalPrice;
        this.ShippingType = ShippingType;
        this.ShippingTypeName = ShippingTypeName;
        this.PaymentType = PaymentType;
        this.PaymentTypeName = PaymentTypeName;
        this.OrderStatus = OrderStatus;
        this.OrderStatusName = OrderStatusName;
        this.UserId = UserId;
        this.ShippingAddress = ShippingAddress;
        this.Materials = Materials;
    }
}