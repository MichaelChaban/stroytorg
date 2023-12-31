using Microsoft.AspNetCore.Mvc;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Host.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    [HttpPost("Register")]
    public async Task<AuthResponse> Register([FromBody] User user)
    {
        return await authService.RegisterAsync(user);
    }

    [HttpPost("Login")]
    public async Task<AuthResponse> Login([FromBody] UserLogin user)
    {
        return await authService.LoginAsync(user);
    }
}
