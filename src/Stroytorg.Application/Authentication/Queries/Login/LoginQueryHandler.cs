using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Extensions;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponse>
    {
        private readonly IUserService userService;
        private readonly ITokenGeneratorService tokenGeneratorService;

        public LoginQueryHandler(
            IUserService userService,
            ITokenGeneratorService tokenGeneratorService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.tokenGeneratorService = tokenGeneratorService ?? throw new ArgumentNullException(nameof(tokenGeneratorService));
        }

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
            return new AuthResponse(
                IsLoggedIn: true,
                JwtToken: tokenGeneratorService.GenerateToken(contractUser.Value));
        }
    }
}
