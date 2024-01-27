using MediatR;
using Stroytorg.Application.Abstractions.Interfaces;
using Stroytorg.Application.Facades.Interfaces;
using Stroytorg.Application.Features.Users.GetUserByEmail;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Authentication.Login;

public class LoginQueryHandler(
    ITokenGeneratorService tokenGeneratorService,
    IOrderFacade orderFacade,
    ISender mediatR)
    : IQueryHandler<LoginQuery, JwtTokenResponse>
{
    private readonly ITokenGeneratorService tokenGeneratorService = tokenGeneratorService ?? throw new ArgumentNullException(nameof(tokenGeneratorService));
    private readonly IOrderFacade orderFacade = orderFacade ?? throw new ArgumentNullException(nameof(orderFacade));
    private readonly ISender mediatR = mediatR ?? throw new ArgumentNullException(nameof(mediatR));

    public async Task<BusinessResult<JwtTokenResponse>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var contractUser = (await mediatR.Send(new GetUserByEmailQuery(query.Email), cancellationToken)).Value;
        await orderFacade.AssignOrderToUserAsync(contractUser);

        return BusinessResult.Success(tokenGeneratorService.GenerateToken(contractUser));
    }
}