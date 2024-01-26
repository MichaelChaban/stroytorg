using FluentValidation;
using Stroytorg.Application.Constants;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Authentication.Register;

internal class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private readonly IUserRepository userRepository;

    public RegisterCommandValidator(IUserRepository userRepository)
    {
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        RuleFor(user => user.Email)
            .MustAsync(UserWithEmailNotExistsAsync)
            .WithMessage(BusinessErrorMessage.ExistingUserWithEmail);
    }

    private async Task<bool> UserWithEmailNotExistsAsync(string email, CancellationToken cancellationToken)
    {
        return !await this.userRepository.ExistsWithEmailAsync(email, cancellationToken);
    }
}