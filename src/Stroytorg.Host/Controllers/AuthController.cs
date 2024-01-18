using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stroytorg.Application.Authentication.Commands.GoogleAuthentication;
using Stroytorg.Application.Authentication.Commands.Register;
using Stroytorg.Application.Authentication.Queries.Login;
using Stroytorg.Application.Constants;
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
        var command = new RegisterCommand(user.Email, user.Password, user.FirstName, user.LastName, user.BirthDate, user.PhoneNumber);
        var authResult = await mediatR.Send(command);

        return !string.IsNullOrEmpty(authResult.AuthErrorMessage)
        ? (authResult.AuthErrorMessage == BusinessErrorMessage.AlreadyExistingUser.ToString()
            ? Conflict(authResult)
            : StatusCode(500, authResult))
            : StatusCode(201, authResult);
    }

    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] UserLogin user)
    {
        var query = new LoginQuery(user.Email, user.Password);
        var result = await mediatR.Send(query);

        return !string.IsNullOrEmpty(result.AuthErrorMessage) ? Unauthorized(result) : Ok(result);
    }

    [HttpPost("GoogleAuth")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> GoogleAuth([FromBody] UserGoogleAuth user)
    {
        var command = new GoogleAuthCommand(user.Token, user.GoogleId, user.Email, user.FirstName, user.LastName);
        var result = await mediatR.Send(command);

        return result.IsLoggedIn ? result : Unauthorized();
    }
}
