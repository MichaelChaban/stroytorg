using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Features.Orders.Commands;
using Stroytorg.Application.Features.Orders.Queries;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(ISender mediatR) : ControllerBase
{
    private readonly ISender mediatR = mediatR ?? throw new ArgumentNullException(nameof(mediatR));

    [HttpGet("PagedUserOrders")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedData<Order>>> GetPagedUserAsync([FromQuery] DataRangeRequest<OrderFilter> request, CancellationToken cancellationToken)
    {
        var query = new GetPagedUserOrderQuery<OrderFilter>(request!.Filter, request!.Sort, request!.Offset, request!.Limit);

        return await mediatR.Send(query, cancellationToken);
    }

    [HttpGet]
    [Authorize(Roles = UserRole.ClientsHandler)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<PagedData<Order>>> GetPagedAsync([FromQuery] DataRangeRequest<OrderFilter> request, CancellationToken cancellationToken)
    {
        var query = new GetPagedOrderQuery<OrderFilter>(request!.Filter, request!.Sort, request!.Offset, request!.Limit);

        return await mediatR.Send(query, cancellationToken);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = UserRole.ClientsHandler)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderDetail>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var query = new GetOrderQuery(id);
        var result = await mediatR.Send(query, cancellationToken);

        return result.IsSuccess ? result.Value : NotFound(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BusinessResponse<int>>> CreateAsync([FromBody] OrderCreate order, CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand(order);
        var result = await mediatR.Send(command, cancellationToken);
        return result.IsSuccess ? StatusCode(201, result) : Conflict(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = UserRole.ClientsHandler)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> UpdateAsync(int id, [FromBody] OrderEdit order, CancellationToken cancellationToken)
    {
        var command = new UpdateOrderCommand(id, order);
        var result = await mediatR.Send(command, cancellationToken);

        return result.IsSuccess ? result.Value : NotFound();
    }
}
