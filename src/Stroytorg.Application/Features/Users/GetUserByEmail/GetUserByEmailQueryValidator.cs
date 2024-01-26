using FluentValidation;
using Stroytorg.Application.Constants;
using Stroytorg.Application.Features.Users.CreateUserWithGoogle;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Users.GetUserByEmail;

internal class GetUserByEmailQueryValidator : AbstractValidator<CreateUserWithGoogleCommand>
{
    private readonly IUserRepository userRepository;

    public GetUserByEmailQueryValidator(IUserRepository userRepository)
    {
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        RuleFor(user => user.Email)
            .MustAsync(UserWithEmailExistsAsync)
            .WithMessage(BusinessErrorMessage.NotExistingUserWithEmail);
    }

    private async Task<bool> UserWithEmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        return await userRepository.ExistsWithEmailAsync(email, cancellationToken);
    }
}
