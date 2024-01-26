using MediatR;
using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Application.Features.Users.GetUserByEmail;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Authentication.Login;

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
        var contractUser = (await mediatR.Send(new GetUserByEmailQuery(query.Email), cancellationToken)).Value;
        await orderFacade.AssignOrderToUserAsync(contractUser);

        return new AuthResponse(
            IsLoggedIn: true,
            JwtToken: tokenGeneratorService.GenerateToken(contractUser));
    }
}