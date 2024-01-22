using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Extensions;
using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Application.Features.Authentication.Queries;
using Stroytorg.Application.Features.Users.Queries;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Authentication.QueryHandlers;

public class LoginQueryHandler(
    ITokenGeneratorService tokenGeneratorService,
    IOrderFacade orderFacade,
    ISender mediatR) :
    IRequestHandler<LoginQuery, AuthResponse>
{
    private readonly ITokenGeneratorService tokenGeneratorService = tokenGeneratorService ?? throw new ArgumentNullException(nameof(tokenGeneratorService));
    private readonly IOrderFacade orderFacade = orderFacade ?? throw new ArgumentNullException(nameof(orderFacade));
    private readonly ISender mediatR = mediatR ?? throw new ArgumentNullException(nameof(mediatR));

    public async Task<AuthResponse> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var contractUser = (await mediatR.Send(new GetUserByEmailQuery(query.User.Email), cancellationToken)).Value;
        if (contractUser is null)
        {
            return new AuthResponse(AuthErrorMessage: BusinessErrorMessage.NotExistingUser);
        }

        if (query.User.Password.VerifyPassword(contractUser.Password!) is false)
        {
            return new AuthResponse(AuthErrorMessage: BusinessErrorMessage.IncorrectPassword);
        }

        await orderFacade.AssignOrderToUserAsync(contractUser);

        return new AuthResponse(
            IsLoggedIn: true,
            JwtToken: tokenGeneratorService.GenerateToken(contractUser));
    }
}