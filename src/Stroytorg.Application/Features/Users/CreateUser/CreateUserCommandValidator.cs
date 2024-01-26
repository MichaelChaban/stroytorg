using FluentValidation;
using Stroytorg.Application.Constants;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Users.CreateUser;

internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IUserRepository userRepository;

    public CreateUserCommandValidator(IUserRepository userRepository)
    {
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        RuleFor(user => user.Email)
            .MustAsync(UserWithEmailNotExistsAsync)
            .WithMessage(BusinessErrorMessage.ExistingUserWithEmail);
    }

    private async Task<bool> UserWithEmailNotExistsAsync(string email, CancellationToken cancellationToken)
    {
        return !await userRepository.ExistsWithEmailAsync(email, cancellationToken);
    }
}
