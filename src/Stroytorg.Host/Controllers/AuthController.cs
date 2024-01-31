using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stroytorg.Application.Features.Authentication.Commands;
using Stroytorg.Application.Features.Authentication.Queries;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(ISender mediatR) : ControllerBase
{
    private readonly ISender mediatR = mediatR ?? throw new ArgumentNullException(nameof(mediatR));

    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] UserRegister user)
    {
        var command = new RegisterCommand(user);
        var result = await mediatR.Send(command);

        return result.IsLoggedIn ? Ok(result) : Unauthorized(result);
    }

    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] UserLogin user)
    {
        var query = new LoginQuery(user);
        var result = await mediatR.Send(query);

        return result.IsLoggedIn ? Ok(result) : Unauthorized(result);
    }

    [HttpPost("GoogleAuth")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> GoogleAuth([FromBody] UserGoogleAuth user)
    {
        var command = new GoogleAuthCommand(user);
        var result = await mediatR.Send(command);

        return result.IsLoggedIn ? Ok(result) : Unauthorized(result);
    }
}
