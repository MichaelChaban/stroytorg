using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Features.Materials.CreateMaterial;
using Stroytorg.Application.Features.Materials.DeleteMaterial;
using Stroytorg.Application.Features.Materials.GetMaterial;
using Stroytorg.Application.Features.Materials.GetPagedMaterial;
using Stroytorg.Application.Features.Materials.UpdateMaterial;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Material;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Host.Abstractions;

namespace Stroytorg.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaterialController(ISender mediatR) : ApiController(mediatR)
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<PagedData<Material>> GetPagedAsync([FromQuery] DataRangeRequest<MaterialFilter> request, CancellationToken cancellationToken)
    {
        var query = new GetPagedMaterialQuery<MaterialFilter>(request!.Filter, request!.Sort, request.Offset, request.Limit);
        return await mediatR.Send(query, cancellationToken);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var query = new GetMaterialQuery(id);
        var result = await mediatR.Send(query, cancellationToken);

        return HandleResult<MaterialDetail>(result);
    }

    [HttpPost]
    [Authorize(Roles = UserRole.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateAsync([FromBody] MaterialEdit material, CancellationToken cancellationToken)
    {
        var command = new CreateMaterialCommand(material.Name, material.Description, material.CategoryId,
            material.Price, material.StockAmount, material.Height, material.Weight, material.Length, material.Weight);
        var result = await mediatR.Send(command, cancellationToken);

        return HandleResult<int>(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = UserRole.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] MaterialEdit material, CancellationToken cancellationToken)
    {
        var command = new UpdateMaterialCommand(id, material.Name, material.Description, material.CategoryId,
            material.Price, material.StockAmount, material.Height, material.Weight, material.Length, material.Weight);
        var result = await mediatR.Send(command, cancellationToken);

        return HandleResult<int>(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRole.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveAsync(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteMaterialCommand(id);
        var result = await mediatR.Send(command, cancellationToken);

        return HandleResult<int>(result);
    }
}
