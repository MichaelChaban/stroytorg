using MediatR;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Users.CreateUser;

public record CreateUserCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    DateTimeOffset BirthDate,
    string PhoneNumber)
    : IRequest<BusinessResult<User>>;
