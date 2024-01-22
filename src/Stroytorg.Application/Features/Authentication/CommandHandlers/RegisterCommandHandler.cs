using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Application.Features.Authentication.Commands;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Features.Authentication.CommandHandlers;

public class RegisterCommandHandler(
    IUserService userService,
    ITokenGeneratorService tokenGeneratorService,
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IOrderFacade orderFacade) :
    IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IUserService userService = userService ?? throw new ArgumentNullException(nameof(userService));
    private readonly ITokenGeneratorService tokenGeneratorService = tokenGeneratorService ?? throw new ArgumentNullException(nameof(tokenGeneratorService));
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IOrderFacade orderFacade = orderFacade ?? throw new ArgumentNullException(nameof(orderFacade));

    public async Task<AuthResponse> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var entityUser = (await userService.GetByEmailAsync(command.Email)).Value;
        if (entityUser is not null)
        {
            return new AuthResponse(AuthErrorMessage: BusinessErrorMessage.AlreadyExistingUser);
        }

        var newUser = autoMapperTypeMapper.Map<RegisterCommand, UserRegister>(command);
        var contractUserResponse = await userService.CreateAsync(newUser);
        if (!contractUserResponse.IsSuccess)
        {
            return new AuthResponse(AuthErrorMessage: contractUserResponse.BusinessErrorMessage);
        }

        await orderFacade.AssignOrderToUserAsync(contractUserResponse.Value);

        return new AuthResponse(
            IsLoggedIn: true,
            JwtToken: tokenGeneratorService.GenerateToken(contractUserResponse.Value));
    }
}
