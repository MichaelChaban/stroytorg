using MediatR;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Users.GetUserByEmail;

public record GetUserByEmailQuery(
    string Email)
    : IRequest<BusinessResult<User>>;
