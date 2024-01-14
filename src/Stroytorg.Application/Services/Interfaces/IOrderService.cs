using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Category;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Services.Interfaces;

public interface IOrderService
{
    Task<PagedData<Order>> GetPagedAsync(DataRangeRequest<OrderFilter> request);

    Task<BusinessResponse<Order>> GetByIdAsync(int categoryId);

    Task<BusinessResponse<int>> CreateAsync(CategoryEdit category);

    Task<BusinessResponse<int>> UpdateAsync(int categoryId, CategoryEdit category);

    Task<BusinessResponse<int>> RemoveAsync(int categoryId);
}
