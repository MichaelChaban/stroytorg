using MediatR;
using Stroytorg.Application.Extensions;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Enums;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Authentication.Commands.GoogleAuthentication;

public class GoogleAuthCommandHandler :
    IRequestHandler<GoogleAuthCommand, AuthResponse>
{
    private readonly IUserService userService;
    private readonly ITokenGeneratorService tokenGeneratorService;
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper;

    public GoogleAuthCommandHandler(
        IUserService userService,
        ITokenGeneratorService tokenGeneratorService,
        IAutoMapperTypeMapper autoMapperTypeMapper)
    {
        this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        this.tokenGeneratorService = tokenGeneratorService ?? throw new ArgumentNullException(nameof(tokenGeneratorService));
        this.autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    }

    public async Task<AuthResponse> Handle(GoogleAuthCommand command, CancellationToken cancellationToken)
    {
        var user = autoMapperTypeMapper.Map<GoogleAuthCommand, UserGoogleAuth>(command);
        var googleValidationResult = await user.ValidateGoogleUserAsync();
        if (googleValidationResult is not null && !googleValidationResult.IsSuccess)
        {
            return new AuthResponse(AuthErrorMessage: googleValidationResult.BusinessErrorMessage);
        }

        var contractUserResponse = await userService.GetByEmailAsync(command.Email);
        if (contractUserResponse.Value is not null && 
            !contractUserResponse.Value.AuthenticationType.ValidateUserAuthType(AuthenticationType.Google, out var businessError))
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
