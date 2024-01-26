using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Features.Orders.CreateOrder;
using Stroytorg.Application.Features.Orders.GetOrder;
using Stroytorg.Application.Features.Orders.GetPagedOrder;
using Stroytorg.Application.Features.Orders.GetPagedUserOrder;
using Stroytorg.Application.Features.Orders.UpdateOrder;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(ISender mediatR) : ControllerBase
{
    private readonly ISender mediatR = mediatR ?? throw new ArgumentNullException(nameof(mediatR));

    [HttpGet("UserPaged")]
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
        return result.IsSuccess ? Ok(result.Value) : NotFound(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BusinessResult<int>>> CreateAsync([FromBody] OrderCreate order, CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand(order.FirstName, order.LastName, order.Email,
            order.PhoneNumber, order.ShippingType, order.PaymentType, order.Materials, order.ShippingAddress);
        var result = await mediatR.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Error);
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
        var command = new UpdateOrderCommand(id, order.OrderStatus);
        var result = await mediatR.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Error);
    }
}
