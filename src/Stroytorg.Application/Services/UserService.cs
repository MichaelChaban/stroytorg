using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Models;

namespace Stroytorg.Application.Services;

public class UserService : IUserService
{
    public async Task<User> GetByIdAsync(int userId)
    {
        await Task.Yield();
        return new User(FirstName: "Mykhailo",
            Id: 1,
            LastName: "Chaban");
    }
}
