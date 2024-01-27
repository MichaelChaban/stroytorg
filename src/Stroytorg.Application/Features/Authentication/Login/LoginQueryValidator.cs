using FluentValidation;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Extensions;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Authentication.Login;

internal class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    private readonly IUserRepository userRepository;

    public LoginQueryValidator(IUserRepository userRepository)
    {
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        RuleFor(user => user.Email)
            .MustAsync(UserExistsWithEmail)
            .WithErrorCode(nameof(LoginQuery.Email))
            .WithMessage(BusinessErrorMessage.NotExistingUserWithEmail);

        RuleFor(user => user)
            .MustAsync(HasValidPassword)
            .WhenAsync(async (user, validationContext, cancellationToken) => await UserExistsWithEmail(user.Email, cancellationToken))
            .WithErrorCode(nameof(LoginQuery.Password))
            .WithMessage(BusinessErrorMessage.InvalidPassword);
    }

    private async Task<bool> UserExistsWithEmail(string email, CancellationToken cancellationToken)
    {
        return await userRepository.ExistsWithEmailAsync(email, cancellationToken);
    }

    private async Task<bool> HasValidPassword(LoginQuery user, CancellationToken cancellationToken)
    {
        var userEntity = await userRepository.GetByEmailAsync(user.Email, cancellationToken);
        return userEntity is not null && user.Password.VerifyPassword(userEntity.Password!);
    }
}