using Stroytorg.Application.Constants;
using Stroytorg.Application.Extensions;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserService userService;
    private readonly IUserRepository userRepository;
    private readonly ITokenGeneratorService tokenGeneratorService;
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper;

    public AuthService(
        IUserService userService,
        IUserRepository userRepository,
        ITokenGeneratorService tokenGeneratorService,
        IAutoMapperTypeMapper autoMapperTypeMapper)
    {
        this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        this.tokenGeneratorService = tokenGeneratorService ?? throw new ArgumentNullException(nameof(tokenGeneratorService));
        this.autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    }

    public async Task<AuthResponse> LoginAsync(UserLogin user)
    {
        var entityUser = await userRepository.GetByEmailAsync(user.Email);
        if (entityUser is null)
        {
            return new AuthResponse(AuthErrorMessage: BusinessErrorMessage.NotExistingUser);
        }

        var isPasswordValid = user.Password.VerifyPassword(entityUser.Password);
        if (!isPasswordValid)
        {
            return new AuthResponse(AuthErrorMessage: BusinessErrorMessage.IncorrectPassword);
        }

        var contractUser = autoMapperTypeMapper.Map<User>(entityUser);

        return new AuthResponse(
            isLogged: true,
            JwtToken: tokenGeneratorService.GenerateToken(contractUser));
    }

    public async Task<AuthResponse> RegisterAsync(User user)
    {
        var entityUser = await userRepository.GetByEmailAsync(user.Email);
        if (entityUser is not null)
        {
            return new AuthResponse(AuthErrorMessage: BusinessErrorMessage.AlreadyExistingUser);
        }

        var userResponse = await userService.CreateAsync(user);
        if (!userResponse.isSuccess)
        {
            return new AuthResponse(AuthErrorMessage: userResponse.BusinessErrorMessage);
        }

        return new AuthResponse(
            isLogged: true,
            JwtToken: tokenGeneratorService.GenerateToken(userResponse.Value));
    }
}
