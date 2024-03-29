﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stroytorg.Application.Features.Authentication.GoogleAuth;
using Stroytorg.Application.Features.Authentication.Login;
using Stroytorg.Application.Features.Authentication.Register;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Host.Abstractions;

namespace Stroytorg.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(ISender mediatR) : ApiController(mediatR)
{
    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Register([FromBody] UserRegister user)
    {
        var command = new RegisterCommand(user.Email, user.Password, user.FirstName, user.LastName, user.BirthDate, user.PhoneNumber);
        var result = await mediatR.Send(command);

        return HandleResult<JwtTokenResponse>(result);
    }

    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] UserLogin user)
    {
        var query = new LoginQuery(user.Email, user.Password);
        var result = await mediatR.Send(query);

        return HandleResult<JwtTokenResponse>(result);
    }

    [HttpPost("GoogleAuth")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GoogleAuth([FromBody] UserGoogleAuth user)
    {
        var command = new GoogleAuthCommand(user.Token, user.GoogleId, user.Email, user.FirstName, user.LastName);
        var result = await mediatR.Send(command);

        return HandleResult<JwtTokenResponse>(result);
    }
}
