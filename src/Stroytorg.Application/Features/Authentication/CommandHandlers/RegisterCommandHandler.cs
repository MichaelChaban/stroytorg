using MediatR;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Features.Authentication.Commands;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Features.Authentication.CommandHandlers;

public class RegisterCommandHandler :
    IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IUserService userService;
    private readonly ITokenGeneratorService tokenGeneratorService;
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper;

    public RegisterCommandHandler(
        IUserService userService,
        ITokenGeneratorService tokenGeneratorService,
        IAutoMapperTypeMapper autoMapperTypeMapper)
    {
        this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        this.tokenGeneratorService = tokenGeneratorService ?? throw new ArgumentNullException(nameof(tokenGeneratorService));
        this.autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    }

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

        return new AuthResponse(
            IsLoggedIn: true,
            JwtToken: tokenGeneratorService.GenerateToken(contractUserResponse.Value));
    }
}
