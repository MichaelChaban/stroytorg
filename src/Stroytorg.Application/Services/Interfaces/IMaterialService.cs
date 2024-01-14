using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Material;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Services.Interfaces;

public interface IMaterialService
{
    Task<PagedData<Material>> GetPagedAsync(DataRangeRequest<MaterialFilter> request);

    Task<BusinessResponse<Material>> GetByIdAsync(int materialId);

    Task<BusinessResponse<int>> CreateAsync(MaterialCreate material);

    Task<BusinessResponse<int>> UpdateAsync(int materialId, MaterialEdit material);

    Task<BusinessResponse<int>> RemoveAsync(int materialId);
}
