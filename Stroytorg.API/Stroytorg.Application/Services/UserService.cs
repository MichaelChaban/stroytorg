using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Models;

namespace Stroytorg.Application.Services;

public class UserService : IUserService
{
    public Task<User> GetByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }
}
