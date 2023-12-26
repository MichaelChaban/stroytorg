using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Models;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper;

    public UserService(IUserRepository userRepository, IAutoMapperTypeMapper autoMapperTypeMapper)
    {
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        this.autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    }

    public async Task<User> GetByIdAsync(int userId)
    {
        var user = await userRepository.GetAsync(userId);
        return autoMapperTypeMapper.Map<User>(user);
    }
}
