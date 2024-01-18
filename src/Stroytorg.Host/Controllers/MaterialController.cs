using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Materials.Commands.CreateMaterial;
using Stroytorg.Application.Materials.Commands.DeleteMaterial;
using Stroytorg.Application.Materials.Commands.UpdateMaterial;
using Stroytorg.Application.Materials.Queries.GetMaterial;
using Stroytorg.Application.Materials.Queries.GetPagedMaterial;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Material;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaterialController : ControllerBase
{
    private readonly ISender mediatR;

    public MaterialController(ISender mediatR)
    {
        this.mediatR = mediatR ?? throw new ArgumentNullException(nameof(mediatR));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedData<Material>>> GetPagedAsync([FromQuery] DataRangeRequest<MaterialFilter> request)
    {
        var query = new GetPagedMaterialQuery<MaterialFilter>(request!.Filter, request!.Sort, request.Offset, request.Limit);
        return await mediatR.Send(query);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Material>> GetByIdAsync(int id)
    {
        var query = new GetMaterialQuery(id);
        var result = await mediatR.Send(query);
        return result.IsSuccess ? result.Value : NotFound();
    }

    [HttpPost]
    [Authorize(Roles = UserRole.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<BusinessResponse<int>>> CreateAsync([FromBody] MaterialCreate material)
    {
        var command = new CreateMaterialCommand(
            material.Name, material.Description, material.CategoryId,
            material.Price, material.StockAmount, material.Height,
            material.Width, material.Length, material.Weight
            );

        return await mediatR.Send(command);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = UserRole.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> UpdateAsync(int id, [FromBody] MaterialEdit material)
    {
        var command = new UpdateMaterialCommand(
            id, material.Name, material.Description,
            material.CategoryId, material.Price, material.StockAmount,
            material.Height, material.Width, material.Length,
            material.Weight, material.IsFavorite
            );

        var result = await mediatR.Send(command);

        return result.IsSuccess ? result.Value : NotFound();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRole.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> RemoveAsync(int id)
    {
        var command = new DeleteMaterialCommand(id);
        var result = await mediatR.Send(command);
        return result.IsSuccess ? result.Value : NotFound();
    }
}
