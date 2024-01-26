using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Features.Users.GetUser;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(ISender mediatR) : ControllerBase
{
    private readonly ISender mediatR = mediatR ?? throw new ArgumentNullException(nameof(mediatR));

    [HttpGet("{id:int}")]
    [Authorize(Roles = UserRole.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BusinessResult<UserDetail>>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var query = new GetUserQuery(id);
        var result = await mediatR.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Error);
    }
}
