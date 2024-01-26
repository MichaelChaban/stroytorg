using FluentValidation;
using Stroytorg.Application.Constants;
using Stroytorg.Domain.Data.Repositories.Interfaces;

namespace Stroytorg.Application.Features.Users.GetUser;

internal class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    private readonly IUserRepository userRepository;

    public GetUserQueryValidator(IUserRepository userRepository)
    {
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        RuleFor(user => user.UserId)
            .MustAsync(UserWithIdExistsAsync)
            .WithMessage(BusinessErrorMessage.NotExistingUserWithId);
    }

    private async Task<bool> UserWithIdExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await userRepository.ExistsAsync(id, cancellationToken);
    }
}
