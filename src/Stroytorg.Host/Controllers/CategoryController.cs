using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stroytorg.Application.Categories.Commands.CreateCategory;
using Stroytorg.Application.Categories.Commands.DeleteCategory;
using Stroytorg.Application.Categories.Commands.UpdateCategory;
using Stroytorg.Application.Categories.Queries.GetCategory;
using Stroytorg.Application.Categories.Queries.GetPagedCategory;
using Stroytorg.Application.Constants;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Category;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ISender mediatR) : ControllerBase
{
    private readonly ISender mediatR = mediatR ?? throw new ArgumentNullException(nameof(mediatR));

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedData<Category>>> GetPagedAsync([FromQuery]DataRangeRequest<CategoryFilter> request)
    {
        var query = new GetPagedCategoryQuery<CategoryFilter>(request!.Filter, request!.Sort, request!.Offset, request!.Limit);

        return await mediatR.Send(query);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Category>> GetByIdAsync(int id)
    {
        var query = new GetCategoryQuery(id);
        var result = await mediatR.Send(query);

        return result.IsSuccess ? result.Value : NotFound(result);
    }

    [HttpPost]
    [Authorize(Roles = UserRole.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<BusinessResponse<int>>> CreateAsync([FromBody]CategoryEdit category)
    {
        var command = new CreateCategoryCommand(category.Name);
        var result = await mediatR.Send(command);

        return result.IsSuccess ? result : Conflict();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = UserRole.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> UpdateAsync(int id, [FromBody]CategoryEdit category)
    {
        var command = new UpdateCategoryCommand(id, category.Name);
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
        var command = new DeleteCategoryCommand(id);
        var result = await mediatR.Send(command);

        return result.IsSuccess ? result.Value : NotFound();
    }
}
