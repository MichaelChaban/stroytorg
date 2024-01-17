using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Material;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaterialController : ControllerBase
{
    private readonly IMaterialService materialService;

    public MaterialController(IMaterialService materialService)
    {
        this.materialService = materialService ?? throw new ArgumentNullException(nameof(materialService));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedData<Material>>> GetPagedAsync([FromQuery] DataRangeRequest<MaterialFilter> request)
    {
        return await materialService.GetPagedAsync(request);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Material>> GetByIdAsync(int id)
    {
        var result = await materialService.GetByIdAsync(id);
        return result.IsSuccess ? result.Value : NotFound();
    }

    [HttpPost]
    [Authorize(Roles = UserRole.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<BusinessResponse<int>>> CreateAsync([FromBody] MaterialCreate material)
    {
        return await materialService.CreateAsync(material);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = UserRole.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> UpdateAsync(int id, [FromBody] MaterialEdit material)
    {
        var result = await materialService.UpdateAsync(id, material);
        return result.IsSuccess ? result.Value : NotFound();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRole.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> RemoveAsync(int id)
    {
        var result = await materialService.RemoveAsync(id);
        return result.IsSuccess ? result.Value : NotFound();
    }
}
