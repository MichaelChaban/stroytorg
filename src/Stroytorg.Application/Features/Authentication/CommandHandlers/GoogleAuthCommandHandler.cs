using MediatR;
using Stroytorg.Application.Extensions;
using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Application.Features.Authentication.Commands;
using Stroytorg.Application.Features.Users.Commands;
using Stroytorg.Application.Features.Users.Queries;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Enums;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Features.Authentication.CommandHandlers;

public class GoogleAuthCommandHandler(
    ITokenGeneratorService tokenGeneratorService,
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IOrderFacade orderFacade,
    ISender mediatR) :
    IRequestHandler<GoogleAuthCommand, AuthResponse>
{
    private readonly ITokenGeneratorService tokenGeneratorService = tokenGeneratorService ?? throw new ArgumentNullException(nameof(tokenGeneratorService));
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IOrderFacade orderFacade = orderFacade ?? throw new ArgumentNullException(nameof(orderFacade));
    private readonly ISender mediatR = mediatR ?? throw new ArgumentNullException(nameof(mediatR));

    public async Task<AuthResponse> Handle(GoogleAuthCommand command, CancellationToken cancellationToken)
    {
        var googleValidationResult = await command.User.ValidateGoogleUserAsync();
        if (googleValidationResult is not null && googleValidationResult.IsSuccess is false)
        {
            return new AuthResponse(AuthErrorMessage: googleValidationResult.BusinessErrorMessage);
        }

        var contractUserResponse = (await mediatR.Send(new GetUserByEmailQuery(command.User.Email), cancellationToken)).Value;
        if (contractUserResponse is not null)
        {
            if (contractUserResponse.AuthenticationType.ValidateUserAuthType(AuthenticationType.Google, out var businessError) is false)
            {
                return new AuthResponse(AuthErrorMessage: businessError!.BusinessErrorMessage);
            }

            await orderFacade.AssignOrderToUserAsync(contractUserResponse);
            return new AuthResponse(
                IsLoggedIn: true,
                JwtToken: tokenGeneratorService.GenerateToken(autoMapperTypeMapper.Map<User>(contractUserResponse)));
        }

        var createdUserResponse = await mediatR.Send(new CreateUserWithGoogleCommand(command.User), cancellationToken);
        if (createdUserResponse.IsSuccess is false)
        {
            return new AuthResponse(AuthErrorMessage: createdUserResponse.BusinessErrorMessage);
        }

        await orderFacade.AssignOrderToUserAsync(createdUserResponse.Value);
        return new AuthResponse(
            IsLoggedIn: true,
            JwtToken: tokenGeneratorService.GenerateToken(autoMapperTypeMapper.Map<User>(createdUserResponse.Value)));
    }
}
