using MediatR;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Users.Queries;

public record GetUserQuery(
    int UserId) : IRequest<BusinessResponse<UserDetail>>;
