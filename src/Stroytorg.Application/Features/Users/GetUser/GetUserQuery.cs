using MediatR;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Infrastructure.Validations.Common;

namespace Stroytorg.Application.Features.Users.GetUser;

public record GetUserQuery(
    int UserId)
    : IRequest<BusinessResult<UserDetail>>;
