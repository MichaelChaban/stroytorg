using MediatR;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Users.CreateUserWithGoogle;

public record CreateUserWithGoogleCommand(
    string Token,
    string GoogleId,
    string Email,
    string FirstName,
    string LastName)
    : IRequest<BusinessResult<User>>;