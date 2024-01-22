using MediatR;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Users.Queries;

public record GetUserByEmailQuery(
    string Email) : IRequest<BusinessResponse<User>>;
