using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(UserLogin user);

    Task<AuthResponse> RegisterAsync(UserRegister user);
}
