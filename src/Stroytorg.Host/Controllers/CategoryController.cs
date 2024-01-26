using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Features.Categories.CreateCategory;
using Stroytorg.Application.Features.Categories.DeleteCategory;
using Stroytorg.Application.Features.Categories.GetCategory;
using Stroytorg.Application.Features.Categories.GetPagedCategory;
using Stroytorg.Application.Features.Categories.UpdateCategory;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Category;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ISender mediatR) : ControllerBase
{
    private readonly ISender mediatR = mediatR ?? throw new ArgumentNullException(nameof(mediatR));

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedData<Category>>> GetPagedAsync([FromQuery]DataRangeRequest<CategoryFilter> request, CancellationToken cancellationToken)
    {
        var query = new GetPagedCategoryQuery<CategoryFilter>(request!.Filter, request!.Sort, request!.Offset, request!.Limit);

        return await mediatR.Send(query, cancellationToken);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDetail>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var query = new GetCategoryQuery(id);
        var result = await mediatR.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Error);
    }

    [HttpPost]
    [Authorize(Roles = UserRole.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<BusinessResult<int>>> CreateAsync([FromBody] CategoryEdit category, CancellationToken cancellationToken)
    {
        var command = new CreateCategoryCommand(category.Name);
        var result = await mediatR.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Error);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = UserRole.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> UpdateAsync(int id, [FromBody]CategoryEdit category, CancellationToken cancellationToken)
    {
        var command = new UpdateCategoryCommand(id, category.Name);
        var result = await mediatR.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Error);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRole.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> RemoveAsync(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteCategoryCommand(id);
        var result = await mediatR.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Error);
    }
}
