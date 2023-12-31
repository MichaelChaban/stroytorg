using Microsoft.AspNetCore.Mvc;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Host.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpGet("{id:int}")]
    public async Task<BusinessResponse<User>> GetByIdAsync([FromQuery] object request, int id)
    {
        return await userService.GetByIdAsync(id);
    }
}
