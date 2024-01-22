using MediatR;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Features.Users.Commands;

public record CreateUserCommand(
    UserRegister User) : IRequest<BusinessResponse<User>>;
