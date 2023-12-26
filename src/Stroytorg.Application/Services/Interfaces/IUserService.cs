using Stroytorg.Contracts.Models;

namespace Stroytorg.Application.Services.Interfaces;

public interface IUserService
{
    Task<User> GetByIdAsync(int userId);
}
