using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Application.Features.Authentication.Commands;
using Stroytorg.Application.Features.Users.Commands;
using Stroytorg.Application.Features.Users.Queries;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Features.Authentication.CommandHandlers;

public class RegisterCommandHandler(
    ITokenGeneratorService tokenGeneratorService,
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IOrderFacade orderFacade,
    ISender mediatR) :
    IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly ITokenGeneratorService tokenGeneratorService = tokenGeneratorService ?? throw new ArgumentNullException(nameof(tokenGeneratorService));
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IOrderFacade orderFacade = orderFacade ?? throw new ArgumentNullException(nameof(orderFacade));
    private readonly ISender mediatR = mediatR ?? throw new ArgumentNullException(nameof(mediatR));

    public async Task<AuthResponse> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {

        var contractUser = (await mediatR.Send(new GetUserByEmailQuery(command.User.Email), cancellationToken)).Value;
        if (contractUser is not null)
        {
            return new AuthResponse(AuthErrorMessage: BusinessErrorMessage.AlreadyExistingUser);
        }

        var newUser = autoMapperTypeMapper.Map<UserRegister>(command.User);
        var contractUserResponse = await mediatR.Send(new CreateUserCommand(newUser), cancellationToken);
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