using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Extensions;
using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Application.Features.Authentication.Queries;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Authentication.QueryHandlers;

public class LoginQueryHandler(
    IUserService userService,
    ITokenGeneratorService tokenGeneratorService,
    IOrderFacade orderFacade) :
    IRequestHandler<LoginQuery, AuthResponse>
{
    private readonly IUserService userService = userService ?? throw new ArgumentNullException(nameof(userService));
    private readonly ITokenGeneratorService tokenGeneratorService = tokenGeneratorService ?? throw new ArgumentNullException(nameof(tokenGeneratorService));
    private readonly IOrderFacade orderFacade = orderFacade ?? throw new ArgumentNullException(nameof(orderFacade));

    public async Task<AuthResponse> Handle(LoginQuery query, CancellationToken cancellationToken)
    {

        var contractUser = await userService.GetByEmailAsync(query.Email);
        if (contractUser.Value is null)
        {
            return new AuthResponse(AuthErrorMessage: BusinessErrorMessage.NotExistingUser);
        }

        if (!query.Password.VerifyPassword(contractUser.Value.Password!))
        {
            return new AuthResponse(AuthErrorMessage: BusinessErrorMessage.IncorrectPassword);
        }

        await orderFacade.AssignOrderToUserAsync(contractUser.Value);

        return new AuthResponse(
            IsLoggedIn: true,
            JwtToken: tokenGeneratorService.GenerateToken(contractUser.Value));
    }
}
