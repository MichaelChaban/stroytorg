using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Services.Interfaces;

public interface IOrderService
{
    Task<PagedData<Order>> GetPagedUserAsync(DataRangeRequest<OrderFilter> request, CancellationToken cancellationToken);

    Task<PagedData<Order>> GetPagedAsync(DataRangeRequest<OrderFilter> request, CancellationToken cancellationToken);

    Task<BusinessResponse<OrderDetail>> GetByIdAsync(int orderId, CancellationToken cancellationToken);

    Task<BusinessResponse<int>> CreateAsync(OrderCreate order, CancellationToken cancellationToken);

    Task<BusinessResponse<int>> UpdateAsync(int orderId, OrderEdit order, CancellationToken cancellationToken);

    Task AssignOrderToUserAsync(User user, CancellationToken cancellationToken);
}
