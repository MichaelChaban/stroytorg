using Stroytorg.Application.Constants;
using Stroytorg.Application.Extensions;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Contracts.Enums;

namespace Stroytorg.Application.Services;

public class AuthService(
    IUserService userService,
    ITokenGeneratorService tokenGeneratorService,
    IAutoMapperTypeMapper autoMapperTypeMapper) : IAuthService
{
    private readonly IUserService userService = userService ?? throw new ArgumentNullException(nameof(userService));
    private readonly ITokenGeneratorService tokenGeneratorService = tokenGeneratorService ?? throw new ArgumentNullException(nameof(tokenGeneratorService));
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));

    public async Task<AuthResponse> LoginAsync(UserLogin user)
    {
        var contractUser = await userService.GetByEmailAsync(user.Email);
        if (contractUser.Value is null)
        {
            return new AuthResponse(AuthErrorMessage: BusinessErrorMessage.NotExistingUser);
        }

        if (!user.Password.VerifyPassword(contractUser.Value.Password!))
        {
            return new AuthResponse(AuthErrorMessage: BusinessErrorMessage.IncorrectPassword);
        }

        return new AuthResponse(
            IsLoggedIn: true,
            JwtToken: tokenGeneratorService.GenerateToken(contractUser.Value));
    }

    public async Task<AuthResponse> RegisterAsync(UserRegister user)
    {
        var entityUser = (await userService.GetByEmailAsync(user.Email)).Value;
        if (entityUser is not null)
        {
            return new AuthResponse(AuthErrorMessage: BusinessErrorMessage.AlreadyExistingUser);
        }

        var contractUserResponse = await userService.CreateAsync(user);
        if (!contractUserResponse.IsSuccess)
        {
            return new AuthResponse(AuthErrorMessage: contractUserResponse.BusinessErrorMessage);
        }

        return new AuthResponse(
            IsLoggedIn: true,
            JwtToken: tokenGeneratorService.GenerateToken(contractUserResponse.Value));
    }

    public async Task<AuthResponse> AuthGoogleAsync(UserGoogleAuth user)
    {
        var googleValidationResult = await user.ValidateGoogleUserAsync();
        if (googleValidationResult is not null && !googleValidationResult.IsSuccess)
        {
            return new AuthResponse(AuthErrorMessage: googleValidationResult.BusinessErrorMessage);
        }

        var contractUserResponse = await userService.GetByEmailAsync(user.Email);
        if (contractUserResponse.Value is not null && !contractUserResponse.Value.AuthenticationType.ValidateUserAuthType(AuthenticationType.Google, out var businessError))
        {
            return new AuthResponse(AuthErrorMessage: businessError!.BusinessErrorMessage);
        }

        if (contractUserResponse.Value is null)
        {
            var createdUserResponse = await userService.CreateWithGoogleAsync(user);
            if (!createdUserResponse.IsSuccess)
            {
                return new AuthResponse(AuthErrorMessage: createdUserResponse.BusinessErrorMessage);
            }

            return new AuthResponse(
                IsLoggedIn: true,
                JwtToken: tokenGeneratorService.GenerateToken(autoMapperTypeMapper.Map<User>(createdUserResponse.Value)));
        }

        return new AuthResponse(
            IsLoggedIn: true,
            JwtToken: tokenGeneratorService.GenerateToken(autoMapperTypeMapper.Map<User>(contractUserResponse.Value)));
    }
}
