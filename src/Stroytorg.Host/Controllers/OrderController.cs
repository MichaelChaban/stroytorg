﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;
using System.Threading;

namespace Stroytorg.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));

    [HttpGet("PagedUserOrders")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedData<Order>>> GetPagedUserAsync([FromQuery] DataRangeRequest<OrderFilter> request, CancellationToken cancellationToken)
    {
        return await orderService.GetPagedUserAsync(request, cancellationToken);
    }

    [HttpGet]
    [Authorize(Roles = UserRole.ClientsHandler)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<PagedData<Order>>> GetPagedAsync([FromQuery] DataRangeRequest<OrderFilter> request, CancellationToken cancellationToken)
    {
        return await orderService.GetPagedAsync(request, cancellationToken);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = UserRole.ClientsHandler)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderDetail>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await orderService.GetByIdAsync(id, cancellationToken);
        return result.IsSuccess ? result.Value : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BusinessResponse<int>>> CreateAsync([FromBody] OrderCreate order, CancellationToken cancellationToken)
    {
        return await orderService.CreateAsync(order, cancellationToken);
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
        var result = await orderService.UpdateAsync(id, order, cancellationToken);
        return result.IsSuccess ? result.Value : NotFound();
    }
}
