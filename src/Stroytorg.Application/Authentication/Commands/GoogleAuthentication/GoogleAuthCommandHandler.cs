using MediatR;
using Stroytorg.Application.Extensions;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Enums;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Authentication.Commands.GoogleAuthentication;

public class GoogleAuthCommandHandler(
    IUserService userService,
    ITokenGeneratorService tokenGeneratorService,
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IOrderService orderService) :
    IRequestHandler<GoogleAuthCommand, AuthResponse>
{
    private readonly IUserService userService = userService ?? throw new ArgumentNullException(nameof(userService));
    private readonly ITokenGeneratorService tokenGeneratorService = tokenGeneratorService ?? throw new ArgumentNullException(nameof(tokenGeneratorService));
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IOrderService orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));

    public async Task<AuthResponse> Handle(GoogleAuthCommand command, CancellationToken cancellationToken)
    {
        var user = autoMapperTypeMapper.Map<GoogleAuthCommand, UserGoogleAuth>(command);
        var googleValidationResult = await user.ValidateGoogleUserAsync();
        if (googleValidationResult is not null && !googleValidationResult.IsSuccess)
        {
            return new AuthResponse(AuthErrorMessage: googleValidationResult.BusinessErrorMessage);
        }

        var contractUserResponse = await userService.GetByEmailAsync(command.Email);
        if (contractUserResponse.Value is not null)
        {
            if (contractUserResponse.Value.AuthenticationType.ValidateUserAuthType(AuthenticationType.Google, out var businessError) is false)
            {
                return new AuthResponse(AuthErrorMessage: businessError!.BusinessErrorMessage);
            }

            await orderService.AssignOrderToUserAsync(contractUserResponse.Value, cancellationToken);
            return new AuthResponse(
                IsLoggedIn: true,
                JwtToken: tokenGeneratorService.GenerateToken(autoMapperTypeMapper.Map<User>(contractUserResponse.Value)));
        }

        var createdUserResponse = await userService.CreateWithGoogleAsync(user);
        if (!createdUserResponse.IsSuccess)
        {
            return new AuthResponse(AuthErrorMessage: createdUserResponse.BusinessErrorMessage);
        }

        await orderService.AssignOrderToUserAsync(createdUserResponse.Value, cancellationToken);

        return new AuthResponse(
            IsLoggedIn: true,
            JwtToken: tokenGeneratorService.GenerateToken(autoMapperTypeMapper.Map<User>(createdUserResponse.Value)));
    }
}
