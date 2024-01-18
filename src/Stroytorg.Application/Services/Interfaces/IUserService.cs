using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using System.Threading;

namespace Stroytorg.Application.Services.Interfaces;

public interface IUserService
{
    Task<BusinessResponse<User>> GetByIdAsync(int userId, CancellationToken cancellationToken = default);

    Task<BusinessResponse<User>> GetByEmailAsync(string email, CancellationToken token = default);

    Task<BusinessResponse<User>> CreateAsync(UserRegister user);

    Task<BusinessResponse<User>> CreateWithGoogleAsync(UserGoogleAuth user);
}
