using MediatR;
using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Application.Extensions;
using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Application.Features.Users.CreateUserWithGoogle;
using Stroytorg.Application.Features.Users.GetUserByEmail;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Enums;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Authentication.GoogleAuth;

public class GoogleAuthCommandHandler(
    ITokenGeneratorService tokenGeneratorService,
    IAutoMapperTypeMapper autoMapperTypeMapper,
    IOrderFacade orderFacade,
    ISender mediatR)
    : ICommandHandler<GoogleAuthCommand, JwtTokenResponse>
{
    private readonly ITokenGeneratorService tokenGeneratorService = tokenGeneratorService ?? throw new ArgumentNullException(nameof(tokenGeneratorService));
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    private readonly IOrderFacade orderFacade = orderFacade ?? throw new ArgumentNullException(nameof(orderFacade));
    private readonly ISender mediatR = mediatR ?? throw new ArgumentNullException(nameof(mediatR));

    public async Task<BusinessResult<JwtTokenResponse>> Handle(GoogleAuthCommand command, CancellationToken cancellationToken)
    {
        var contractUserResponse = (await mediatR.Send(new GetUserByEmailQuery(command.Email), cancellationToken)).Value;
        if (contractUserResponse is not null)
        {
            if (contractUserResponse.AuthenticationType.ValidateUserAuthType(AuthenticationType.Google, out var businessError) is false)
            {
                return BusinessResult.Failure<JwtTokenResponse>(businessError!.Error!);
            }

            await orderFacade.AssignOrderToUserAsync(contractUserResponse);
            return BusinessResult.Success(tokenGeneratorService.GenerateToken(autoMapperTypeMapper.Map<User>(contractUserResponse)));
        }

        var createdUserResponse = await CreateUserWithGoogle(command, cancellationToken);
        if (createdUserResponse.IsFailure)
        {
            return BusinessResult.Failure<JwtTokenResponse>(createdUserResponse.Error!);
        }

        await orderFacade.AssignOrderToUserAsync(createdUserResponse.Value);

        return BusinessResult.Success(tokenGeneratorService.GenerateToken(autoMapperTypeMapper.Map<User>(createdUserResponse.Value)));
    }

    private async Task<BusinessResult<User>> CreateUserWithGoogle(GoogleAuthCommand command, CancellationToken cancellationToken)
    {
        return await mediatR.Send(
            new CreateUserWithGoogleCommand(
                command.Token,
                command.GoogleId,
                command.Email,
                command.FirstName,
                command.LastName),
            cancellationToken);
    }
}
