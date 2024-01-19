using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Category;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Services.Interfaces;

public interface ICategoryService
{
    Task<PagedData<Category>> GetPagedAsync(DataRangeRequest<CategoryFilter> request);

    Task<BusinessResponse<CategoryDetail>> GetByIdAsync(int categoryId);

    Task<BusinessResponse<int>> CreateAsync(CategoryEdit category);

    Task<BusinessResponse<int>> UpdateAsync(int categoryId, CategoryEdit category);

    Task<BusinessResponse<int>> RemoveAsync(int categoryId);
}
