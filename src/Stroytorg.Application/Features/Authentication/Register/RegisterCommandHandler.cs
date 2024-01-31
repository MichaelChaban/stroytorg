using MediatR;
using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Application.Features.Users.CreateUser;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Authentication.Register;

public class RegisterCommandHandler(
    ITokenGeneratorService tokenGeneratorService,
    IOrderFacade orderFacade,
    ISender mediatR)
    : ICommandHandler<RegisterCommand, JwtTokenResponse>
{
    private readonly ITokenGeneratorService tokenGeneratorService = tokenGeneratorService ?? throw new ArgumentNullException(nameof(tokenGeneratorService));
    private readonly IOrderFacade orderFacade = orderFacade ?? throw new ArgumentNullException(nameof(orderFacade));
    private readonly ISender mediatR = mediatR ?? throw new ArgumentNullException(nameof(mediatR));

    public async Task<BusinessResult<JwtTokenResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var contractUserResponse = await mediatR.Send(
            new CreateUserCommand(command.Email, command.Password, command.FirstName, command.LastName, command.BirthDate, command.PhoneNumber), 
            cancellationToken);

        if (contractUserResponse.IsFailure)
        {
            return BusinessResult.Failure<JwtTokenResponse>(contractUserResponse.Error!);
        }

        await orderFacade.AssignOrderToUserAsync(contractUserResponse.Value);

        return BusinessResult.Success(tokenGeneratorService.GenerateToken(contractUserResponse.Value));
    }
}