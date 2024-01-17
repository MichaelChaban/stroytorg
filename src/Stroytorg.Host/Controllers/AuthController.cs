using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stroytorg.Application.Authentication.Commands.GoogleAuthentication;
using Stroytorg.Application.Authentication.Commands.Register;
using Stroytorg.Application.Authentication.Queries.Login;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator mediator;

    public AuthController(IMediator mediator)
    {
        this.mediator = mediator ?? throw new ArgumentNullException();
    }

    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] UserRegister user)
    {
        var command = new RegisterCommand(user.Email, user.Password, user.FirstName, user.LastName, user.BirthDate, user.PhoneNumber);
        var authResult = await mediator.Send(command);
        return authResult;
    }

    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] UserLogin user)
    {
        var query = new LoginQuery(user.Email, user.Password);
        var result = await mediator.Send(query);
        return result;
    }

    [HttpPost("GoogleAuth")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> GoogleAuth([FromBody] UserGoogleAuth user)
    {
        var command = new GoogleAuthCommand(user.Token, user.GoogleId, user.Email, user.FirstName, user.LastName);
        var result = await mediator.Send(command);
        return result.IsLoggedIn ? result : Unauthorized();
    }
}
