﻿using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Services.Interfaces;

public interface IOrderService
{
    Task<PagedData<Order>> GetPagedForUserAsync(DataRangeRequest<OrderFilter> request);

    Task<PagedData<Order>> GetPagedAsync(DataRangeRequest<OrderFilter> request);

    Task<BusinessResponse<Order>> GetByIdAsync(int orderId);

    Task<BusinessResponse<int>> CreateAsync(OrderCreate order);

    Task<BusinessResponse<int>> UpdateAsync(int orderId, OrderEdit order);
}
